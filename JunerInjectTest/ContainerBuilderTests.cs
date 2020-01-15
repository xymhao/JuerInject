using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BindingFlags = System.Reflection.BindingFlags;

namespace JunerInjectTest
{
    [TestClass]
    public class ContainerBuilderTests
    {
        private Container _container;

        public ContainerBuilderTests()
        {
            _container = new Container();
            _container.Register<IPerson, Person>();
            _container.Register<IStudent, Student>();
        }

        //todo 创建容器
        //todo 注入实例
        //todo 获取实例
        [TestMethod]
        public void Should_Create_Container()
        {

            var person = _container.Resolve<IPerson>();
            Assert.IsTrue(person is Person);
        }

        [TestMethod]
        public void Should_Get_Parameters_Objects_By_Type()
        {
            object[] objects = _container.GetParametersObjects(typeof(Person));
            Assert.AreEqual(1, objects.Length);
        }

        [TestMethod]
        public void Should_Implementation_ChildType_Student()
        {
            object[] objects = _container.GetParametersObjects(typeof(Person));
            Assert.IsTrue(objects[0] is Student);
        }

        [TestMethod]
        public void Should_Get_Child_Object_By_ParameterInfo()
        {
            var ctor = typeof(Person).GetConstructors().FirstOrDefault();
            var parameterInfos = ctor.GetParameters();
            var child = _container.ResolveCtor(parameterInfos.First().ParameterType);
            Assert.IsTrue(child is Student);
        }
    }

    public class Person: IPerson
    {
        public Person(IStudent student = null)
        {
            Student = student;
        }

        public IStudent Student { get; set; }
    }

    public class Student : IStudent
    {

    }

    public interface IStudent
    {
    }

    public interface IPerson
    {
        public IStudent Student { get; set; }

    }
}
