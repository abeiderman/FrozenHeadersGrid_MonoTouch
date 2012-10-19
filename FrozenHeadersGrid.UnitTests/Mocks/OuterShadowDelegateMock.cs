using System;

namespace FrozenHeadersGrid.UnitTests
{
    public class OuterShadowDelegateMock : IOuterShadowDelegate
    {
        public Func<float> LeftShadowStartXDelegate { get; set; }
        public Func<float> TopShadowStartYDelegate { get; set; }
        public Func<float> RightShadowStartXDelegate { get; set; }
        public Func<float> BottomShadowStartYDelegate { get; set; }

        public float LeftShadowStartX
        {
            get 
            {
                if (LeftShadowStartXDelegate != null)
                    return LeftShadowStartXDelegate();
                else
                    return 0;
            }
        }

        public float RightShadowStartX
        {
            get 
            {
                if (RightShadowStartXDelegate != null)
                    return RightShadowStartXDelegate();
                else
                    return 0; 
            }
        }

        public float TopShadowStartY
        {
            get 
            { 
                if (TopShadowStartYDelegate != null)
                    return TopShadowStartYDelegate();
                else
                    return 0; 
            }
        }

        public float BottomShadowStartY
        {
            get
            {
                if (BottomShadowStartYDelegate != null)
                    return BottomShadowStartYDelegate();
                else
                    return 0;
            }
        }
    }
}

