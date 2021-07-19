using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using FastMember;
using System.Reflection;

namespace FastMemberBenchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
        }
    }

    public abstract class TestRunner
    {
        protected Account _account;
        protected TypeAccessor _typeAccessor;
        protected ObjectAccessor _objectAccessor;
        protected PropertyInfo _nameProp, _passwordProp, _balanceProp;

        [GlobalSetup]
        public void Setup()
        {
            _account = new Account();
            _typeAccessor = TypeAccessor.Create(typeof(Account), true);
            _objectAccessor = ObjectAccessor.Create(_account, true);
            _nameProp = GetProperty(nameof(Account.Name));
            _passwordProp = GetProperty(nameof(Account.Password));
            _balanceProp = GetProperty(nameof(Account.Balance));
        }

        private PropertyInfo GetProperty(string propName) =>
            typeof(Account).GetProperty(propName, BindingFlags.Instance | BindingFlags.Public);
    }

    [MemoryDiagnoser]
    public class PublicGetRunner : TestRunner
    {
        [Benchmark]
        public string FastMember_TypeAccessor_PublicGet() =>
            _typeAccessor[_account, nameof(Account.Name)] as string;

        [Benchmark]
        public string FastMember_ObjectAccessor_PublicGet() =>
            _objectAccessor[nameof(Account.Name)] as string;

        [Benchmark]
        public string Static_PublicGet() => _account.Name;

        [Benchmark]
        public string Reflection_PublicGet() => _nameProp.GetValue(_account) as string;
    }

    [MemoryDiagnoser]
    public class PublicSetRunner : TestRunner
    {
        [Benchmark]
        public void FastMember_TypeAccessor_PublicSet() =>
            _typeAccessor[_account, nameof(Account.Name)] = "Nguyen Thai Duong";

        [Benchmark]
        public void FastMember_ObjectAccessor_PublicSet() =>
            _objectAccessor[nameof(Account.Name)] = "Nguyen Thai Duong";

        [Benchmark]
        public void Static_PublicSet() => _account.Name = "Nguyen Thai Duong";

        [Benchmark]
        public void Reflection_PublicSet() => _nameProp.SetValue(_account, "Nguyen Thai Duong");
    }

    [MemoryDiagnoser]
    public class PrivateGetRunner : TestRunner
    {
        [Benchmark]
        public string FastMember_TypeAccessor_PrivateGet() =>
            _typeAccessor[_account, nameof(Account.Password)] as string;

        [Benchmark]
        public string FastMember_ObjectAccessor_PrivateGet() =>
            _objectAccessor[nameof(Account.Password)] as string;

        [Benchmark]
        public string Reflection_PrivateGet() => _passwordProp.GetValue(_account) as string;
    }

    [MemoryDiagnoser]
    public class PrivateSetRunner : TestRunner
    {
        [Benchmark]
        public void FastMember_TypeAccessor_PrivateSet() =>
            _typeAccessor[_account, nameof(Account.Balance)] = 2_000;

        [Benchmark]
        public void FastMember_ObjectAccessor_PrivateSet() =>
            _objectAccessor[nameof(Account.Balance)] = 2_000;

        [Benchmark]
        public void Reflection_PrivateSet() => _balanceProp.SetValue(_account, 2_000);
    }

    [MemoryDiagnoser]
    public class AccessorCreateRunner
    {
        private Account _account;

        [GlobalSetup]
        public void Setup() => _account = new Account();

        [Benchmark]
        public TypeAccessor TypeAccessor_Create_DisallowNonPublic() =>
            TypeAccessor.Create(typeof(Account));

        [Benchmark]
        public TypeAccessor TypeAccessor_Create_AllowNonPublic() =>
            TypeAccessor.Create(typeof(Account), true);

        [Benchmark]
        public ObjectAccessor ObjectAccessor_Create_DisallowNonPublic() =>
            ObjectAccessor.Create(_account);

        [Benchmark]
        public ObjectAccessor ObjectAccessor_Create_AllowNonPublic() =>
            ObjectAccessor.Create(_account, true);
    }
}
