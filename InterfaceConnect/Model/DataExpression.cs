using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace InterfaceConnect
{
    class DataExpression: IExperssion
    {
        private Dictionary<string, DataSingleRule> _expressions = new Dictionary<string, DataSingleRule>();
        public DataExpression(List<DataSingleRule> arr)
        {
            foreach (DataSingleRule exp in arr)
            {
                if (string.IsNullOrEmpty(exp.Key))
                {
                    continue;
                }
                _expressions[exp.Key] = exp;
            }
        }
        public Dictionary<string,object> Interpret(Dictionary<string,object> data)
        {
            if (_expressions == null || _expressions.Count == 0)
            {
                return data;
            }
            data = CaculateDic(data, _expressions, "", null, null);
            return data;
        }
        // 使用缓存来优化运算速度
        private Dictionary<string, CacheExpression> Cache = new Dictionary<string, CacheExpression>();
        class CacheExpression
        {
            public string Sql { get; set; }
            public Dictionary<string, DataSingleRule> Rules { get; set; }
        }
        private Dictionary<string,object> CaculateDic(Dictionary<string, object> data, Dictionary<string, DataSingleRule> rules,string fatherKey, DataSingleRule fatherRule, Dictionary<string,string> uncleDic)
        {
            var strRules = rules.Where(x => x.Value.KeyType == KeyType.OBJECT).ToDictionary(kv => kv.Key,kv=>kv.Value);
            var listRules = rules.Where(x => x.Value.KeyType == KeyType.ARRAY).ToDictionary(kv => kv.Key, kv => kv.Value);
            foreach(var pair in listRules)
            {
                var sonRules = GetSonRules(rules, pair.Key);
                foreach(var strPair in sonRules)
                {
                    strRules.Remove(strPair.Key);
                }
            }
            //Object
            var strData = data.ToDictionary(kv => kv.Key, kv => JsonTool.TryToString(kv.Value));
            if (uncleDic != null) strData = strData.Union(uncleDic).ToDictionary(kv => kv.Key, kv => (string)kv.Value);

            {//执行表达式

                // select表达式
                var selectRules = strRules.Where(x => x.Value.Formula.ToUpper().StartsWith("SELECT")).ToDictionary(kv => kv.Key, kv => kv.Value);
                if (selectRules.Count > 0)
                {
                    foreach (var pair in selectRules)
                    {
                        var selectSql = BuildSelectSql(strData, pair.Value.Formula);
                        data = SelectData(data, strRules, selectSql, pair.Key);
                    }
                    strData = data.ToDictionary(kv => kv.Key, kv => JsonTool.TryToString(kv.Value));
                    if (uncleDic != null) strData = strData.Union(uncleDic).ToDictionary(kv => kv.Key, kv => (string)kv.Value);
                    strRules = strRules.Except(selectRules).ToDictionary(kv => kv.Key, kv => kv.Value);
                }

                var firstKeys = new Dictionary<string, DataSingleRule>();
                var dependenciesSql = string.Empty;

                // 使用缓存，不重复生成SQL以及寻找依赖关系，从以加快程序的运行速度
                var cacheKey = fatherKey;
                if(string.IsNullOrWhiteSpace(cacheKey))
                {
                    cacheKey = "[[root]]";
                }
                if (Cache.ContainsKey(cacheKey))
                {
                    dependenciesSql = Cache[cacheKey].Sql;
                    firstKeys = Cache[cacheKey].Rules;
                }
                else
                {
                    dependenciesSql = BuildDependenciesSql(strRules, ref firstKeys);
                    Cache[cacheKey] = new CacheExpression() { Sql = dependenciesSql, Rules = firstKeys };
                }

                if (IsIncludeFormula(strRules, fatherRule))
                {   
                    var exeSql = string.Empty;
                    // 一般表达式
                    exeSql = BuildExeSql(strData, firstKeys, dependenciesSql, fatherKey);
                    // where表达式
                    if (fatherRule != null && fatherRule.Formula.Trim().StartsWith("where"))
                    {
                        var whereSql = BuildWhereSql(strData, fatherRule.Formula);
                        exeSql = $"select * from ({exeSql}) {whereSql}";
                    }
                    data = FillData(data, exeSql, fatherKey);
                    if (data == null)
                    {
                        return data;
                    }
                }
            }
            // Array
            var listData = data.Where(v => v.Value.GetType().Name == "List`1").ToDictionary(kv => kv.Key, kv => (List<Dictionary<string, object>>)kv.Value);
            foreach (var pair in listRules)
            {
                var sonData = new List<Dictionary<string, object>>();
                if (listData.ContainsKey(pair.Key))
                {
                    sonData = (List<Dictionary<string, object>>)data[pair.Key];
                }
                var sonRules = GetSonRules(rules, pair.Key);

                string keyPath = string.IsNullOrEmpty(fatherKey) ? pair.Key : fatherKey + "." + pair.Key;
                // select 表达式
                var rule = rules[pair.Key];
                if (rule != null)
                {
                    if (rule.Formula.Trim().StartsWith("select"))
                    {
                        var selectSql = BuildSelectSql(strData, rule.Formula);
                        sonData = SelectData(sonData, sonRules, selectSql, keyPath);
                    }
                }
                for(int i=0; i < sonData.Count ;i++)
                {
                    var sonDic = sonData[i];
                    sonData[i] = CaculateDic(sonDic, sonRules, keyPath, rules[pair.Key], strData);
                }
                sonData.RemoveAll((x) => { return x == null; });
                data[pair.Key] = sonData;
            }
            return data;
        }
        private bool IsIncludeFormula(Dictionary<string, DataSingleRule> rules, DataSingleRule fatherRule)
        {
            if((rules == null || rules.Count ==0) && (fatherRule == null || string.IsNullOrWhiteSpace(fatherRule.Formula)))
            {
                return false;
            }
            var flag = false;
            foreach(var rule in rules)
            {
                if(!string.IsNullOrWhiteSpace(rule.Value.Formula))
                {
                    flag = true;
                }
            }
            return flag;
        }
        private Dictionary<string,object> ConvertDataType(Dictionary<string, object> data, Dictionary<string, DataSingleRule> rules)
        {
            if (data == null || rules == null) return data;

            foreach(var pair in rules)
            {
                var key = pair.Key;
                var rule = pair.Value;

                if(data.ContainsKey(key))
                {
                    var obj = data[key];
                    if(rule.KeyType == KeyType.STRING)
                    {
                        data[key] = obj.ToString();
                    }
                    else if(rule.KeyType == KeyType.OBJECT)
                    {
                        data[key] = obj.ToString();
                    }
                    else if(rule.KeyType == KeyType.NUMBER)
                    {
                        try
                        {
                            data[key] = Convert.ToInt32(obj);
                        }
                        catch(Exception ex)
                        {
                            data[key] =  obj.ToString() + ",To Number Error!";
                        }
                    }
                    else if(rule.KeyType == KeyType.BOOL)
                    {
                        if(obj.ToString() == "1" || obj.ToString().ToUpper() == "TRUE")
                        {
                            data[key] = true;
                        }
                        else if(obj.ToString() == "0" || obj.ToString().ToUpper() == "FALSE")
                        {
                            data[key] = false;
                        }
                    }
                }
            }
            return data;
        }
        private Dictionary<string,object> SelectData(Dictionary<string,object> data,Dictionary<string,DataSingleRule> rules,string selectSql,string fatherKey)
        {
            var message = string.Empty;
            DataTable dt = RisOracle.SelectSql(selectSql,ref message);
            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception("Sql执行过程中遇到了异常，ExeSql=" + selectSql + "\n" + message);
            }
            if (dt == null)
            {
                return data;
            }
            if (dt.Rows.Count == 0)
            {
                return data;
            }
            var row = dt.Rows[0];
            var sonRules = rules.Where(x => x.Key.StartsWith(fatherKey + "."));
            foreach (DataColumn column in dt.Columns)
            {
                foreach (var pair in sonRules)
                {
                    string key = string.IsNullOrEmpty(fatherKey) ? pair.Key : pair.Key.Substring(fatherKey.Length + 1, pair.Key.Length - fatherKey.Length - 1);
                    if (key.ToUpper().Equals(column.ColumnName.ToUpper()))
                    {
                        if (pair.Value.KeyType == KeyType.ARRAY)
                        {
                            continue;
                        }
                        var key2 = fatherKey + "." + key;
                        data[key2] =  row[column.ColumnName] is DBNull ? "" : Convert.ToString(row[column.ColumnName]);
                    }
                }
            }
            return data;
        }
        private List<Dictionary<string,object>> SelectData(List<Dictionary<string, object>> data,Dictionary<string,DataSingleRule> rules, string selectSql,string fatherKey)
        {
            var message = string.Empty;
            DataTable dt = RisOracle.SelectSql(selectSql, ref message);
            if (!string.IsNullOrEmpty(message))
            {
                throw new Exception("Sql执行过程中遇到了异常，ExeSql=" + selectSql + "\n" + message);
            }
            if (dt == null)
            {
                return data;
            }
            if (dt.Rows.Count == 0)
            {
                return data;
            }
            foreach(DataRow row in dt.Rows)
            {
                var dic = new Dictionary<string, object>();
                foreach(DataColumn column in dt.Columns)
                {
                    foreach(var pair in rules)
                    {
                        string key = string.IsNullOrEmpty(fatherKey) ? pair.Key : pair.Key.Substring(fatherKey.Length + 1, pair.Key.Length - fatherKey.Length - 1);
                        if(key.ToUpper().Equals(column.ColumnName.ToUpper()))
                        {
                            if (pair.Value.KeyType == KeyType.ARRAY)
                            {
                                continue;
                            }
                            dic.Add(key, row[column.ColumnName] is DBNull ? "" : Convert.ToString(row[column.ColumnName].ToString()));
                        }
                    }
                }
                data.Add(dic);
            }
            return data;
        }
        private Dictionary<string, DataSingleRule> GetSonRules(Dictionary<string, DataSingleRule> exps, string fatherKey)
        {
            var childrenExps = new Dictionary<string, DataSingleRule>();
            foreach (var pair in exps)
            {
                if (pair.Key.StartsWith(fatherKey + ".") && pair.Key != fatherKey)
                {
                    childrenExps.Add(pair.Key, pair.Value);
                }
            }
            return childrenExps;
        }
        private Dictionary<string, object> FillData(Dictionary<string,object> data,string exeSql,string fatherKey)
        {
            string message = string.Empty;
            DataTable dt = RisOracle.SelectSql(exeSql, ref message);
            if( ! string.IsNullOrEmpty(message))
            {
                throw new Exception("Sql执行过程中遇到了异常，ExeSql=" +  exeSql + "\n" + message);
            }
            if(dt == null)
            {
                return data;
            }
            if(dt.Rows.Count == 0)
            {
                return null;
            }
            DataRow row = dt.Rows[0];
            foreach(DataColumn column in dt.Columns)
            {
                string key = string.IsNullOrEmpty(fatherKey) ? column.ColumnName : column.ColumnName.Substring(fatherKey.Length + 1, column.ColumnName.Length - fatherKey.Length - 1);

                if (data.ContainsKey(key))
                {
                    if (data[key] is List<object>)
                    {
                        continue;
                    }
                    data[key] = row[column.ColumnName] is DBNull ? "" : Convert.ToString(row[column.ColumnName]);
                }
                else if(key == "DUMMY")
                {
                    continue;
                }
                else
                {
                    data.Add(key, row[column.ColumnName] is DBNull ? "" : Convert.ToString(row[column.ColumnName]));
                }
            }
            return data;
        }
        private string BuildWhereSql(Dictionary<string, string> data, string whereSql)
        {
            var keys = GetKeysInExpression(whereSql);
            foreach (var key in keys)
            {
                if (data.ContainsKey(key))
                {
                    whereSql = whereSql.Replace($"\"{key}\"", "'" + data[key].ToString() + "'");
                }
            }
            return whereSql;
        }
        private string BuildSelectSql(Dictionary<string, string> data, string selectSql)
        {
            var keys = GetKeysInExpression(selectSql);

            foreach (var key in keys)
            {
                if (data.ContainsKey(key))
                {
                    selectSql = selectSql.Replace($"\"{key}\"", "'" + data[key].ToString() + "'");
                }
            }
            return selectSql;
        }
        private string BuildDependenciesSql(Dictionary<string, DataSingleRule> rules, ref Dictionary<string, DataSingleRule> firstKeys)
        {
            var expKeys = new Dictionary<string, HashSet<String>>();
            foreach (var pair in rules)
            {
                expKeys.Add(pair.Key, GetKeysInExpression(pair.Value.Formula));
            }
            // 1、找到关键字之间的依赖关系
            var dependencies = GetDependenciesInKeys(expKeys);
            if (dependencies == null || dependencies.Count == 0)
            {
                return string.Empty;
            }
            // 2、获取第一层关键字
            firstKeys = rules.Where(x => dependencies[0].Contains(x.Key)).ToDictionary(kv => kv.Key, kv => kv.Value);
            // 3、组建关系SQL语句
            return GetSqlByDependencies(dependencies, rules);
        }
        private string BuildExeSql(Dictionary<string, string> dic, Dictionary<string, DataSingleRule> firstKeys,string dependenciesSql,string fatherKey)
        {
            List<string> temp = new List<string>();
            foreach (var pair in firstKeys)
            {
                var str = string.Empty;

                var expKeys = GetKeysInExpression(pair.Value.Formula);

                if (!string.IsNullOrEmpty(pair.Value.Formula.Trim()) && expKeys.Count == 0 )
                {
                    str = pair.Value.Formula;
                }
                else if(expKeys.Count > 0 && !expKeys.Contains(pair.Key))
                {
                    str = pair.Value.Formula;
                    foreach (var key in dic.Keys)
                    {
                        foreach (var expKey in expKeys)
                        {
                            if(key == expKey)
                            {
                                str = str.Replace($"\"{key}\"","'" + dic[key].Replace("'", "''") + "'");
                            }
                        }
                    }  
                }
                else
                {
                    string value = string.Empty;
                    foreach(var key in dic.Keys)
                    {
                        if (pair.Key == (string.IsNullOrEmpty(fatherKey) ? key: fatherKey + '.' + key))
                        {
                            value = dic[key].Replace("'", "''") ;
                            break;
                        }
                    }
                    str = $"'{value}'";
                }
                str += $" \"{pair.Key}\"";
                temp.Add(str);
            }

            var exeSql = string.Empty;
            if(temp != null && temp.Count > 0)
            {
                exeSql = dependenciesSql.Replace(FirstLineTag, string.Join(",", temp));
            }
            else
            {
                exeSql = dependenciesSql.Replace(FirstLineTag, "*");
            }
            return exeSql;
        }

        private static string FirstLineTag = "[(First)]";

        private string GetSqlByDependencies(List<HashSet<string>> dependencies, Dictionary<string, DataSingleRule> exps)
        {
            string sql = "select " + FirstLineTag + " \nfrom dual where rownum=1";

            HashSet<string> selectKeys = new HashSet<string>(dependencies[0]);

            List<string> temp = new List<string>();
            for (int i = 1;i < dependencies.Count; i++)
            {
                var keys = dependencies[i];

                temp = new List<string>();
                foreach (string key in keys)
                {
                    string str = exps[key].Formula + " " + "\"" + key + "\"";
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
        // 寻找关键字之间的关系
        private static List<HashSet<string>> GetDependenciesInKeys(Dictionary<string, HashSet<string>> expKeys)
        {
            var result = new List<HashSet<string>>();
            var firstDependence = new HashSet<string>();

            var interDependence = new HashSet<string>();
            var keys = new HashSet<string>(expKeys.Keys);

            var first = -1;
            while (keys.Count > 0)
            {
                if (first == keys.Count)
                {
                    break;
                }
                first = keys.Count;

                var dependence = new HashSet<string>();
                foreach (var key in keys.ToList())
                {
                    var value = expKeys[key];
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
                if(dependence.Count > 0)
                {
                    result.Add(dependence);
                }
            }
            if (keys.Count > 0)
            {
                firstDependence = new HashSet<string>(firstDependence.Union(keys));
            }
            result.Insert(0, firstDependence);
            return result;
        }
        private static HashSet<string> GetKeysInExpression(string expression)
        {
            HashSet<string> keys = new HashSet<string>();
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
                    keys.Add(expression.Substring(left + 1, right - left - 1));
                }
            }
            if (st.Count > 0)
            {
                throw new Exception("该表达式编写错误，请仔细检查！" + expression);
            }
            return keys;
        }

    }
}
