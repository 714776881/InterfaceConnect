using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace APIConfigService
{
    public class ExpressionEvaluator2
    {
        private Dictionary<string,Expression> _expressions;

        public ExpressionEvaluator2(List<Expression> expressions)
        {
            _expressions = new Dictionary<string, Expression>();
            foreach (Expression exp in expressions)
            {
                if(string.IsNullOrEmpty(exp.key))
                {
                    continue;
                }
                _expressions.Add(exp.key, exp);
            }
        }
        public Dictionary<string, object> calculate(Dictionary<string, object> dic)
        {
            if (_expressions == null || _expressions.Count == 0)
            {
                return dic;
            }
            Dictionary<string, Expression> strExps = new Dictionary<string, Expression>();
            Dictionary<string, Expression> listExps = new Dictionary<string, Expression>();

            foreach (string key in _expressions.Keys)
            {
                Expression expr = _expressions[key];
                if (dic.ContainsKey(key))
                {
                    if (dic[key] is List<object>)
                    {
                        listExps.Add(key, expr);
                        continue;
                    }
                }
                strExps.Add(key, expr);
            }
            // 将值注入关键字
            foreach(string key in strExps.Keys)
            {
                Expression expr = strExps[key];
                if (dic.ContainsKey(key))
                {
                    expr.value = (string)dic[key];
                }
                else
                {
                    expr.value = string.Empty;
                }
            }
            dic = EvaluatorStrExpression(strExps, dic);
            return dic;
        }

        public Dictionary<string,object> CalculateDic(Dictionary<string,object> dic)
        {
            // 1、根据类型分开进行处理
            Dictionary<string, Expression> strExps = new Dictionary<string, Expression>();

            HashSet<string> arrExps = new HashSet<string>();

            Dictionary<string, Expression> listExps = new Dictionary<string, Expression>();

            foreach (string key in _expressions.Keys)
            {
                Expression expr = _expressions[key];
                if (dic.ContainsKey(key))
                {
                    if (dic[key] is List<object>)
                    {
                        arrExps.Add(key);
                        continue;
                    }
                }
                strExps.Add(key, expr);
            }
            // 2、处理Dicationary<string,string>
            foreach (string key in strExps.Keys)
            {
                Expression expr = strExps[key];
                if (dic.ContainsKey(key))
                {
                    expr.value = (string)dic[key];
                }
                else
                {
                    expr.value = string.Empty;
                }
            }
            dic = EvaluatorStrExpression(strExps, dic);
            // 3、处理Dicationary<string,List<Dictionary<string,Object>>>
            foreach(string key in arrExps)
            {
                List<Dictionary<string, object>> tempList = (List<Dictionary<string, object>>)dic[key];
                for(int i = 0; i < tempList.Count ;i++)
                {
                    tempList[i] = CalculateDic(tempList[i]);
                }
                dic[key] = tempList;
            }
            return dic;
        }

        private Dictionary<string,object> EvaluatorStrExpression(Dictionary<string,Expression> dicExps, Dictionary<string, object> dic)
        {
            // 1、找到表达式依赖关系
            Dictionary<string, List<string>> dicExpsKeys = new Dictionary<string, List<string>>();
            foreach (string key in dicExps.Keys)
            {
                Expression exp = dicExps[key];
                dicExpsKeys.Add(key, GetExpressionKey(exp.expression));
            }
            List<HashSet<string>> dependences = GetDependence(dicExpsKeys);
            if(dependences == null || dependences.Count == 0)
            {
                return dic;
            }
            // 2、根据依赖关系，组建SQL语句
            string sql = GetSqlByDependence(dependences, dicExps, dicExpsKeys);
            // 3、将计算出来的SQL语句注入Dictionary
            dic = FillInDictionary(dic,sql);
            return dic;
        }

        // 1、获取关键字的表达式计算的依赖关系
        // 其中，列表中的每个元素都是一个哈希集合，表示一级依赖关系。输出的集合上层依赖下层，下层不依赖上层。
        // 第一个哈希集合 firstDependence 表示不依赖于任何其他键的键，其余哈希集合表示依赖于前一级别的键的键集合。
        private List<HashSet<string>> GetDependence(Dictionary<string, List<string>> gradeDic)
        {
            var result = new List<HashSet<string>>();
            var firstDependence = new HashSet<string>();

            var interDependence = new HashSet<string>();
            var keys = new HashSet<string>(gradeDic.Keys);

            while (keys.Count > 0)
            {
                var dependence = new HashSet<string>();
                foreach (var key in keys.ToList())
                {
                    var value = gradeDic[key];
                    // 第一层
                    if (value.Count == 0)
                    {
                        interDependence.Add(key);
                        keys.Remove(key);
                        firstDependence.Add(key);
                        continue;
                    }
                    if (value.Contains(key))
                    {
                        firstDependence.Add(key);
                    }

                    // 其余层 
                    var temp = new HashSet<string>(interDependence);
                    if (value.Contains(key))
                    {
                        temp.Add(key);
                    }
                    var flag = value.All(v => temp.Contains(v));
                    if (flag)
                    {
                        dependence.Add(key);
                        keys.Remove(key);
                    }
                }
                interDependence.UnionWith(dependence);
                result.Add(dependence);
            }

            result.Insert(0, firstDependence);
            return result;
        }


        // 2、根据依赖关系，组建SQL表达式语句
        private string GetSqlByDependence(List<HashSet<string>> dependences, Dictionary<string, Expression> dicExps, Dictionary<string, List<string>> dicExpsKeys)
        {
            string sql = string.Empty;
            HashSet<string> selectKeys = new HashSet<string>();
            // 1、组成第一层SQL
            HashSet<string> first = dependences[0];
            List<string> temp = new List<string>();
            foreach (string key in first)
            {
                string str = string.Empty;
                // 既有值又有表达式，优先进行表达式计算
                if (dicExpsKeys[key].Count == 0 && !string.IsNullOrEmpty(dicExps[key].expression))
                {
                    str = dicExps[key].expression + " " + "\"" + dicExps[key].key + "\"";
                }
                else
                {
                    str = "\'" + dicExps[key].value + "\'" + " " + "\"" + key + "\"";
                }
                temp.Add(str);
                selectKeys.Add(key);
            }
            sql = "select " + string.Join(",", temp) + " \nfrom dual where rownum=1";
            // 2、组成其余层
            for (int i = 1; i < dependences.Count; i++)
            {
                HashSet<string> keys = dependences[i];

                temp = new List<string>();
                foreach (string key in keys)
                {
                    string str = dicExps[key].expression + " " + "\"" + key + "\"";
                    temp.Add(str);
                }

                selectKeys.ExceptWith(keys);
                foreach (string key in selectKeys)
                {
                    string str = "\"" + key + "\"";
                    temp.Add(str);
                }
                selectKeys.UnionWith(keys);

                sql = "select " + string.Join(",", temp) + " \nfrom (" + sql + ")";
            }
            return sql;
        }

        // 3、在RIS库中运行SQl，并将计算结果注入Dictionary
        public Dictionary<string, object> FillInDictionary(Dictionary<string, object> dic, string sql)
        {
            string message = string.Empty;
            DataTable dt = OracleHelper.SelectSql(sql, ref message);
            if (dt == null || dt.Rows.Count == 0)
            {
                return dic;
            }
            DataRow row = dt.Rows[0];
            foreach (DataColumn column in dt.Columns)
            {
                string key = column.ColumnName;
                if (dic.ContainsKey(key))
                {
                    if(dic[key] is List<object>)
                    {
                        continue;
                    }
                    dic[key] = row[key] is DBNull ? "" : Convert.ToString(row[key]);
                }
                else
                {
                    dic.Add(key, row[key] is DBNull ? "" : Convert.ToString(row[key]));
                }
            }
            return dic;
        }
        // 获取表达式中的关键字列表
        private List<string> GetExpressionKey(string expression)
        {
            List<string> keys = new List<string>();
            Stack<int> st = new Stack<int>();
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '\"')
                {
                    st.Push(i);
                }
                if (st.Count() > 1)
                {
                    int right = st.Pop();
                    int left = st.Pop();
                    keys.Add(expression.Substring(left + 1, right-left-1));
                }
            }
            if(st.Count > 0)
            {
                throw new Exception("该表达式编写错误，请仔细检查！" + expression);
            }
            return keys;
        }
    }
}


//// 获取关键字之间的表达式依赖关系
//private List<HashSet<string>> getDependence(Dictionary<string, List<string>> gradeDic)
//{
//    var interDependence = new HashSet<string>();
//    var firstDependence = new HashSet<string>();
//    var keys = new HashSet<string>(gradeDic.Keys);
//    foreach (var key in keys.ToList())
//    {
//        var value = gradeDic[key];
//        if (value.Count == 0)
//        {
//            interDependence.Add(key);
//            gradeDic.Remove(key);
//        }
//        else 
//        {
//            if (value.Contains(key))
//            {
//                firstDependence.Add(key);
//            }
//        }
//    }
//    var  dependencies = new List<HashSet<string>>();
//    firstDependence.UnionWith(interDependence);
//    dependencies.Add(firstDependence);
//    return findInterDependence(dependencies, gradeDic, interDependence);
//}

//private List<HashSet<string>> findInterDependence(List<HashSet<string>> dependencies, Dictionary<string, List<string>> dic, HashSet<string> interDependence)
//{
//    if (dic.Count == 0)
//    {
//        return dependencies;
//    }
//    HashSet<string> dependence = new HashSet<string>();
//    HashSet<string> keys = new HashSet<string>(dic.Keys);
//    foreach (var key in keys.ToList())
//    {
//        var exps = dic[key];
//        var temp = new HashSet<string>(interDependence);
//        if (exps.Contains(key))
//        {
//            temp.Add(key);
//        }
//        var flag = exps.All(v => temp.Contains(v));
//        if (flag)
//        {
//            dependence.Add(key);
//            dic.Remove(key);
//        }
//    }
//    interDependence.UnionWith(dependence);
//    dependencies.Add(dependence);
//    return findInterDependence(dependencies, dic, interDependence);
//}