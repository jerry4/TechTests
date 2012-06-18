using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;
using Xunit;

namespace DynamicKeyword
{
  public class BasicDynamicTest
  {
    [Fact]
    public void XUnitTest()
    {
      Assert.True(true);
    }

    [Fact]
    public void CallMethodThatDoesNotExistThrows()
    {
      dynamic a = "hi there";
      Assert.Throws<RuntimeBinderException>(() => a.HeyThere());
    }

    [Fact]
    public void CallPropertyThatDoesNotExistThrows()
    {
      dynamic a = "hi there";
      Assert.Throws<RuntimeBinderException>(() =>
                                              {
                                                string b = a.Test;
                                              });
    }

    [Fact]
    public void CanCheckToSeeIfThingExists()
    {
      dynamic a = "hi there";
      // no good way to do this.  Try catch???
    }

    public class CustomDynamic : DynamicObject
    {
      public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
      {
        bool success = base.TryInvokeMember(binder, args, out result);

        // If the method didn't exist, ensure the result is null
        if (!success) result = null;

        // Always return true to avoid Exceptions being raised
        return true;
      }
    }
    
    [Fact]
    public void CustomDynamicHasProperty()
    {
      // not great rhs has to be a specific type of class that derives from DynamicObject
      dynamic a = new CustomDynamic();
      Assert.DoesNotThrow(() => a.Test() );
    }

    [Fact]
    public void DictionaryWithDynamicValue()
    {
      var dict = new Dictionary<string, dynamic>();
      dict["test"] = new Url("http://google.com");
      Url storedDynamic = dict["test"];
      Assert.Equal("http://google.com", storedDynamic.Value);
    }

    [Fact]
    public void ExpandoObjectForTests()
    {
      dynamic dict = new ExpandoObject(); // var not good enough. must be dynamic
      dict.test = 1;
      // some time later
      int actual = dict.test;
      Assert.Equal(1, actual);
    }
  }
}
