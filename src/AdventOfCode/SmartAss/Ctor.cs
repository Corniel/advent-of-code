using System.Reflection;

namespace SmartAss;

public static class Ctor
{
    public static T New<T>(IEnumerable parameters)
    {
        var pars = parameters.Cast<object>().ToArray();
        var ctors = typeof(T).GetConstructors();
        var ctor = ctors.Length > 1
            ? typeof(T).GetConstructors().First(c => Matches(c, pars))
            : ctors.First();

        return (T)ctor.Invoke(pars);

        static bool Matches(ConstructorInfo info, object[] pars)
        {
            var parameters = info.GetParameters();
            return parameters.Length == pars.Length
                && parameters.Where((p, i) => p.ParameterType == pars[i].GetType()).Count() == pars.Length;
        }
    }
}
