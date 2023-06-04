﻿namespace BLang
{
    public class OneCharSyntaxTokenAttribute : Attribute
    {
        public char C;

        public OneCharSyntaxTokenAttribute(char c1)
        {
            C = c1;
        }
    }
    
    public class TwoCharSyntaxTokenAttribute : Attribute
    {
        public char C1;
        public char C2;

        public TwoCharSyntaxTokenAttribute(char c1, char c2)
        {
            C1 = c1;
            C2 = c2;
        }   
    }

    public class ThreeCharSyntaxTokenAttribute : Attribute
    {
        public char C1;
        public char C2;
        public char C3;

        public ThreeCharSyntaxTokenAttribute(eTwoCharSyntaxToken prefix, char c3) 
        {
            C1 = prefix.Char1();
            C2 = prefix.Char2();
            C3 = c3;
        }
    }

    public static class SyntaxTokenAttributeData
    {
        private const int ONE_CHAR_TOKEN_START = 2000;
        private const int TWO_CHAR_TOKEN_START = 3000;
        private const int THREE_CHAR_TOKEN_START = 4000; 

        public static char Char(this eOneCharSyntaxToken token)
        {
            return mCacheHelper.GetAttribute(token).C;
        }

        public static char Char1(this eTwoCharSyntaxToken token)
        {
            return mTwoCharCacheHelper.GetAttribute(token).C1;
        }

        public static char Char2(this eTwoCharSyntaxToken token)
        {
            return mTwoCharCacheHelper.GetAttribute(token).C2;
        }

        public static char Char1(this eThreeCharSyntaxToken token)
        {
            return mThreeCharCacheHelper.GetAttribute(token).C1;
        }

        public static char Char2(this eThreeCharSyntaxToken token)
        {
            return mThreeCharCacheHelper.GetAttribute(token).C2;
        }

        public static char Char3(this eThreeCharSyntaxToken token)
        {
            return mThreeCharCacheHelper.GetAttribute(token).C3;
        }

        public static int Code(this eOneCharSyntaxToken token)
        {
            return (int)token + ONE_CHAR_TOKEN_START;
        }

        public static int Code(this eTwoCharSyntaxToken token)
        {
            return (int)token + TWO_CHAR_TOKEN_START;
        }

        public static int Code(this eThreeCharSyntaxToken token)
        {
            return (int)token + THREE_CHAR_TOKEN_START;
        }

        private static readonly AttributeCacheHelper<OneCharSyntaxTokenAttribute, eOneCharSyntaxToken> mCacheHelper = new();
        private static readonly AttributeCacheHelper<TwoCharSyntaxTokenAttribute, eTwoCharSyntaxToken> mTwoCharCacheHelper = new();
        private static readonly AttributeCacheHelper<ThreeCharSyntaxTokenAttribute, eThreeCharSyntaxToken> mThreeCharCacheHelper = new();
    }
}
