using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
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
  }
}
