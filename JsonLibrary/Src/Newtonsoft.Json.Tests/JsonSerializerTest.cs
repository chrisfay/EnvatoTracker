#region License
// Copyright (c) 2007 James Newton-King
//
// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Newtonsoft.Json;
using System.IO;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Tests
{
  public class Product
  {
    public string Name;
    public DateTime Expiry;
    public decimal Price;
    public string[] Sizes;
  }

  public class Store
  {
    public StoreColor Color = StoreColor.Yellow;
    public DateTime Establised = new DateTime(2010, 1, 22);
    public double Width = 1.1;
    public int Employees = 999;
    public int[] RoomsPerFloor = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    public bool Open = false;
    public char Symbol = '@';
    public List<string> Mottos = new List<string>();
    public decimal Cost = 100980.1M;
    public string Escape = "\r\n\t\f\b?{\\r\\n\"\'";
    public List<Product> product = new List<Product>();

    public Store()
    {
      Mottos.Add("Hello World");
      Mottos.Add("������\\'{new Date(12345);}[222]_�@��~");
      Mottos.Add(null);
      Mottos.Add(" ");

      Product rocket = new Product();
      rocket.Name = "Rocket";
      rocket.Expiry = new DateTime(2000, 2, 2, 23, 1, 30);
      Product alien = new Product();
      alien.Name = "Alien";

      product.Add(rocket);
      product.Add(alien);
    }
  }

  public enum StoreColor
  {
    Black,
    Red,
    Yellow,
    White
  }

  [TestFixture]
  public class JsonSerializerTest
  {
    [Test]
    public void PersonTypedObjectDeserialization()
    {
      Store store = new Store();

      string jsonText = JavaScriptConvert.SerializeObject(store);

      Store deserializedStore = (Store)JavaScriptConvert.DeserializeObject(jsonText, typeof(Store));

      Console.WriteLine(jsonText);
    }

    [Test]
    public void TypedObjectDeserialization()
    {
      Product product = new Product();

      product.Name = "Apple";
      product.Expiry = new DateTime(2008, 12, 28);
      product.Price = 3.99M;
      product.Sizes = new string[] { "Small", "Medium", "Large" };

      string output = JavaScriptConvert.SerializeObject(product);
      //{
      //  "Name": "Apple",
      //  "Expiry": new Date(1230422400000),
      //  "Price": 3.99,
      //  "Sizes": [
      //    "Small",
      //    "Medium",
      //    "Large"
      //  ]
      //}

      Product deserializedProduct = (Product)JavaScriptConvert.DeserializeObject(output, typeof(Product));

      Assert.AreEqual("Apple", deserializedProduct.Name);
      Assert.AreEqual(new DateTime(2008, 12, 28), deserializedProduct.Expiry);
      Assert.AreEqual(3.99, deserializedProduct.Price);
      Assert.AreEqual("Small", deserializedProduct.Sizes[0]);
      Assert.AreEqual("Medium", deserializedProduct.Sizes[1]);
      Assert.AreEqual("Large", deserializedProduct.Sizes[2]);
    }

    [Test]
    public void JavaScriptConvertSerializer()
    {
      string value = @"{""Name"":""Orange"", ""Price"":3.99, ""Expiry"":""01/24/2010 12:00:00""}";

      Product p = JavaScriptConvert.DeserializeObject(value, typeof(Product)) as Product;

      Assert.AreEqual("Orange", p.Name);
      Assert.AreEqual(new DateTime(2010, 1, 24, 12, 0, 0), p.Expiry);
      Assert.AreEqual(3.99, p.Price);
    }

    [Test]
    public void DeserializeJavaScriptDate()
    {
      DateTime dateValue = new DateTime(2000, 3, 30);
      Dictionary<string, object> testDictionary = new Dictionary<string, object>();
      testDictionary["date"] = dateValue;

      string jsonText = JavaScriptConvert.SerializeObject(testDictionary);

      Dictionary<string, object> deserializedDictionary = (Dictionary<string, object>)JavaScriptConvert.DeserializeObject(jsonText, typeof(Dictionary<string, object>));
      DateTime deserializedDate = (DateTime)deserializedDictionary["date"];

      Assert.AreEqual(dateValue, deserializedDate);

      Console.WriteLine("DeserializeJavaScriptDate");
      Console.WriteLine(jsonText);
      Console.WriteLine();
      Console.WriteLine(jsonText);
    }

    public class MethodExecutorObject
    {
      public string serverClassName;
      public object[] serverMethodParams;
      public string clientGetResultFunction;
    }

    [Test]
    public void TestMethodExecutorObject()
    {
      MethodExecutorObject executorObject = new MethodExecutorObject();
      executorObject.serverClassName = "BanSubs";
      executorObject.serverMethodParams = new object[] { "21321546", "101", "1236", "D:\\1.txt" };
      executorObject.clientGetResultFunction = "ClientBanSubsCB";

      string output = JavaScriptConvert.SerializeObject(executorObject);

      MethodExecutorObject executorObject2 = JavaScriptConvert.DeserializeObject(output, typeof(MethodExecutorObject)) as MethodExecutorObject;

      Assert.AreNotSame(executorObject, executorObject2);
      Assert.AreEqual(executorObject2.serverClassName, "BanSubs");
      Assert.AreEqual(executorObject2.serverMethodParams.Length, 4);
      Assert.Contains("101", executorObject2.serverMethodParams);
      Assert.AreEqual(executorObject2.clientGetResultFunction, "ClientBanSubsCB");
    }

    [Test]
    public void HashtableDeserialization()
    {
      string value = @"{""Name"":""Orange"", ""Price"":3.99, ""Expiry"":""01/24/2010 12:00:00""}";

      Hashtable p = JavaScriptConvert.DeserializeObject(value, typeof(Hashtable)) as Hashtable;

      Assert.AreEqual("Orange", p["Name"].ToString());
    }

    public class TypedSubHashtable
    {
      public string Name;
      public Hashtable Hash;
    }

    [Test]
    public void TypedHashtableDeserialization()
    {
      string value = @"{""Name"":""Orange"", ""Hash"":{""Expiry"":""01/24/2010 12:00:00""}}";

      TypedSubHashtable p = JavaScriptConvert.DeserializeObject(value, typeof(TypedSubHashtable)) as TypedSubHashtable;

      Assert.AreEqual("01/24/2010 12:00:00", p.Hash["Expiry"].ToString());
    }

    public class GetOnlyPropertyClass
    {
      public string Field = "Field";

      public string GetOnlyProperty
      {
        get { return "GetOnlyProperty"; }
      }
    }

    [Test]
    public void SerializeDeserializeGetOnlyProperty()
    {
      string value = JavaScriptConvert.SerializeObject(new GetOnlyPropertyClass());

      GetOnlyPropertyClass c = JavaScriptConvert.DeserializeObject<GetOnlyPropertyClass>(value);

      Assert.AreEqual(c.Field, "Field");
      Assert.AreEqual(c.GetOnlyProperty, "GetOnlyProperty");
    }

    public class SetOnlyPropertyClass
    {
      public string Field = "Field";

      public string SetOnlyProperty
      {
        set { }
      }
    }

    [Test]
    public void SerializeDeserializeSetOnlyProperty()
    {
      string value = JavaScriptConvert.SerializeObject(new SetOnlyPropertyClass());

      SetOnlyPropertyClass c = JavaScriptConvert.DeserializeObject<SetOnlyPropertyClass>(value);

      Assert.AreEqual(c.Field, "Field");
    }

    public class JsonIgnoreAttributeTestClass
    {
      private int _property = 21;
      private int _ignoredProperty = 12;

      public int Field;
      public int Property
      {
        get { return _property; }
      }

      [JsonIgnore]
      public int IgnoredField;

      [JsonIgnore]
      public int IgnoredProperty
      {
        get { return _ignoredProperty; }
      }
    }

    [Test]
    public void JsonIgnoreAttributeTest()
    {
      string json = JavaScriptConvert.SerializeObject(new JsonIgnoreAttributeTestClass());

      Assert.AreEqual(@"{""Field"":0,""Property"":21}", json);

      JsonIgnoreAttributeTestClass c = JavaScriptConvert.DeserializeObject<JsonIgnoreAttributeTestClass>(@"{""Field"":99,""Property"":-1,""IgnoredField"":-1}");

      Assert.AreEqual(0, c.IgnoredField);
      Assert.AreEqual(99, c.Field);
    }

    [Test]
    public void GoogleSearchAPI()
    {
      string json = @"{
    results:
        [
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://www.google.com/"",
                url : ""http://www.google.com/"",
                visibleUrl : ""www.google.com"",
                cacheUrl : 
""http://www.google.com/search?q=cache:zhool8dxBV4J:www.google.com"",
                title : ""Google"",
                titleNoFormatting : ""Google"",
                content : ""Enables users to search the Web, Usenet, and 
images. Features include PageRank,   caching and translation of 
results, and an option to find similar pages.""
            },
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://news.google.com/"",
                url : ""http://news.google.com/"",
                visibleUrl : ""news.google.com"",
                cacheUrl : 
""http://www.google.com/search?q=cache:Va_XShOz_twJ:news.google.com"",
                title : ""Google News"",
                titleNoFormatting : ""Google News"",
                content : ""Aggregated headlines and a search engine of many of the world's news sources.""
            },
            
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://groups.google.com/"",
                url : ""http://groups.google.com/"",
                visibleUrl : ""groups.google.com"",
                cacheUrl : 
""http://www.google.com/search?q=cache:x2uPD3hfkn0J:groups.google.com"",
                title : ""Google Groups"",
                titleNoFormatting : ""Google Groups"",
                content : ""Enables users to search and browse the Usenet 
archives which consist of over 700   million messages, and post new 
comments.""
            },
            
            {
                GsearchResultClass:""GwebSearch"",
                unescapedUrl : ""http://maps.google.com/"",
                url : ""http://maps.google.com/"",
                visibleUrl : ""maps.google.com"",
                cacheUrl : 
""http://www.google.com/search?q=cache:dkf5u2twBXIJ:maps.google.com"",
                title : ""Google Maps"",
                titleNoFormatting : ""Google Maps"",
                content : ""Provides directions, interactive maps, and 
satellite/aerial imagery of the United   States. Can also search by 
keyword such as type of business.""
            }
        ],
        
    adResults:
        [
            {
                GsearchResultClass:""GwebSearch.ad"",
                title : ""Gartner Symposium/ITxpo"",
                content1 : ""Meet brilliant Gartner IT analysts"",
                content2 : ""20-23 May 2007- Barcelona, Spain"",
                url : 
""http://www.google.com/url?sa=L&ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB&num=1&q=http://www.gartner.com/it/sym/2007/spr8/spr8.jsp%3Fsrc%3D_spain_07_%26WT.srch%3D1&usg=__CxRH06E4Xvm9Muq13S4MgMtnziY="", 

                impressionUrl : 
""http://www.google.com/uds/css/ad-indicator-on.gif?ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB"", 

                unescapedUrl : 
""http://www.google.com/url?sa=L&ai=BVualExYGRo3hD5ianAPJvejjD8-s6ye7kdTwArbI4gTAlrECEAEYASDXtMMFOAFQubWAjvr_____AWDXw_4EiAEBmAEAyAEBgAIB&num=1&q=http://www.gartner.com/it/sym/2007/spr8/spr8.jsp%3Fsrc%3D_spain_07_%26WT.srch%3D1&usg=__CxRH06E4Xvm9Muq13S4MgMtnziY="", 

                visibleUrl : ""www.gartner.com""
            }
        ]
}
";
      object o = JavaScriptConvert.DeserializeObject(json);
      string s = string.Empty;
      s += s;
    }

    [Test]
    public void TorrentDeserializeTest()
    {
      string jsonText = @"{
"""":"""",
""label"": [
       [""SomeName"",6]
],
""torrents"": [
       [""192D99A5C943555CB7F00A852821CF6D6DB3008A"",201,""filename.avi"",178311826,1000,178311826,72815250,408,1603,7,121430,""NameOfLabelPrevioslyDefined"",3,6,0,8,128954,-1,0],
],
""torrentc"": ""1816000723""
}";

      JavaScriptObject o = (JavaScriptObject)JavaScriptConvert.DeserializeObject(jsonText);
      Assert.AreEqual(4, o.Count);

      JavaScriptArray torrentsArray = (JavaScriptArray)o["torrents"];
      JavaScriptArray nestedTorrentsArray = (JavaScriptArray)torrentsArray[0];
      Assert.AreEqual(nestedTorrentsArray.Count, 19);
    }

    public class JsonPropertyClass
    {
      [JsonProperty("pie")]
      public string Pie = "Yum";

      [JsonIgnore]
      public string pie = "No pie for you!";

      public string pie1 = "PieChart!";

      private int _sweetCakesCount;

      [JsonProperty("sweet_cakes_count")]
      public int SweetCakesCount
      {
        get { return _sweetCakesCount; }
        set { _sweetCakesCount = value; }
      }
    }

    [Test]
    public void JsonPropertyClassSerialize()
    {
      JsonPropertyClass test = new JsonPropertyClass();
      test.Pie = "Delicious";
      test.SweetCakesCount = int.MaxValue;

      string jsonText = JavaScriptConvert.SerializeObject(test);

      Assert.AreEqual(@"{""pie"":""Delicious"",""pie1"":""PieChart!"",""sweet_cakes_count"":2147483647}", jsonText);

      JsonPropertyClass test2 = JavaScriptConvert.DeserializeObject<JsonPropertyClass>(jsonText);

      Assert.AreEqual(test.Pie, test2.Pie);
      Assert.AreEqual(test.SweetCakesCount, test2.SweetCakesCount);
    }

    public class BadJsonPropertyClass
    {
      [JsonProperty("pie")]
      public string Pie = "Yum";

      public string pie = "PieChart!";
    }

    [Test]
    [ExpectedException(typeof(JsonSerializationException), ExpectedMessage = @"A member with the name 'pie' already exists on Newtonsoft.Json.Tests.JsonSerializerTest+BadJsonPropertyClass. Use the JsonPropertyAttribute to specify another name.")]
    public void BadJsonPropertyClassSerialize()
    {
      JavaScriptConvert.SerializeObject(new BadJsonPropertyClass());
    }

    public class Article
    {
      public string Name;

      public Article()
      {
      }

      public Article(string name)
      {
        Name = name;
      }
    }

    public class ArticleCollection : List<Article>
    {
    }

    [Test]
    public void InheritedListSerialize()
    {
      Article a1 = new Article("a1");
      Article a2 = new Article("a2");

      ArticleCollection articles1 = new ArticleCollection();
      articles1.Add(a1);
      articles1.Add(a2);

      string jsonText = JavaScriptConvert.SerializeObject(articles1);

      ArticleCollection articles2 = JavaScriptConvert.DeserializeObject<ArticleCollection>(jsonText);

      Assert.AreEqual(articles1.Count, articles2.Count);
      Assert.AreEqual(articles1[0].Name, articles2[0].Name);
    }

    [Test]
    public void ReadOnlyCollectionSerialize()
    {
      ReadOnlyCollection<int> r1 = new ReadOnlyCollection<int>(new int[] {0, 1, 2, 3, 4});

      string jsonText = JavaScriptConvert.SerializeObject(r1);

      ReadOnlyCollection<int> r2 = JavaScriptConvert.DeserializeObject<ReadOnlyCollection<int>>(jsonText);

      CollectionAssert.AreEqual(r1, r2);
    }

    public class Person
    {
      private Guid _internalId;
      private string _firstName;

      [JsonIgnore]
      public Guid InternalId
      {
        get { return _internalId; }
        set { _internalId = value; }
      }

      [JsonProperty("first_name")]
      public string FirstName
      {
        get { return _firstName; }
        set { _firstName = value; }
      }
    }

    //public abstract class Foo
    //{
    //}

    //public class Baz : Foo
    //{
    //}

    //public class Bar : Foo
    //{
    //}

    //public class BazJsonConverter : JsonConverter
    //{
    //  public override bool CanConvert(Type objectType)
    //  {
    //    throw new Exception("The method or operation is not implemented.");
    //  }
    //}

    //public class BarJsonConverter : JsonConverter
    //{
    //  public override bool CanConvert(Type objectType)
    //  {
    //    throw new Exception("The method or operation is not implemented.");
    //  }
    //}
  }
}