using System;
using System.Collections.Generic;
using System.Text;

namespace TabletTesting
{
    public class TestClass {
        public int num { get; set; }
        public string str1 { get; set; }
        public string str2 { get; set; }

        public TestClass(int n, string s1, string s2){
            num = n;
            str1 = s1;
            str2 = s2;
        }
    }
}
