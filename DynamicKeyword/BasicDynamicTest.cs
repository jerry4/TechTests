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


  }
}
