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
        protected PropertyInfo _nameProp;

        [GlobalSetup]
        public void Setup()
        {
            _account = new Account();
            _typeAccessor = TypeAccessor.Create(typeof(Account));
            _objectAccessor = ObjectAccessor.Create(_account);
            _nameProp = _account.GetType().GetProperty("Name", BindingFlags.Instance | BindingFlags.Public);
        }
    }

    [MemoryDiagnoser]
    public class PublicGetRunner : TestRunner
    {
        [Benchmark]
        public string FastMember_TypeAccessor_PublicGet()
        {
            return _typeAccessor[_account, "Name"] as string;
        }

        [Benchmark]
        public string FastMember_ObjectAccessor_PublicGet()
        {
            return _objectAccessor["Name"] as string;
        }

        [Benchmark]
        public string Static_PublicGet()
        {
            return _account.Name;
        }

        [Benchmark]
        public string Reflection_PublicGet()
        {
            return _nameProp.GetValue(_account) as string;
        }
    }

    [MemoryDiagnoser]
    public class PublicSetRunner : TestRunner
    {
        [Benchmark]
        public void FastMember_TypeAccessor_PublicSet()
        {
            _typeAccessor[_account, "Name"] = "Nguyen Thai Duong";
        }

        [Benchmark]
        public void FastMember_ObjectAccessor_PublicSet()
        {
            _objectAccessor["Name"] = "Nguyen Thai Duong";
        }

        [Benchmark]
        public void Static_PublicSet()
        {
            _account.Name = "Nguyen Thai Duong";
        }

        [Benchmark]
        public void Reflection_PublicSet()
        {
            _nameProp.SetValue(_account, "Nguyen Thai Duong");
        }
    }

    [MemoryDiagnoser]
    public class PrivateGetRunner : TestRunner
    {
        [Benchmark]
        public string FastMember_TypeAccessor_PrivateGet()
        {
            return _typeAccessor[_account, "Password"] as string;
        }

        [Benchmark]
        public string FastMember_ObjectAccessor_PrivateGet()
        {
            return _objectAccessor["Password"] as string;
        }

        [Benchmark]
        public string Reflection_PrivateGet()
        {
            return _nameProp.GetValue(_account) as string;
        }
    }

    [MemoryDiagnoser]
    public class PrivateSetRunner : TestRunner
    {
        [Benchmark]
        public void FastMember_TypeAccessor_PrivateSet()
        {
            _typeAccessor[_account, "Balance"] = 2_000;
        }

        [Benchmark]
        public void FastMember_ObjectAccessor_PrivateSet()
        {
            _objectAccessor["Balance"] = 2_000;
        }

        [Benchmark]
        public void Reflection_PrivateSet()
        {
            _nameProp.SetValue(_account, 2_000);
        }
    }
}
