using System.Collections;
using System.Collections.Generic;

public enum Element
{
    NONE, FIRE, EARTH, WATER, WIND, ELECTRIC, ICE, LAVA, SAND, GLASS
}

//public class ElementCalculator
//{
//    static public List<Element> Weakness(Element elem)
//    {
//        List<Element> weakness =new List<Element>();
//        switch (elem)
//        {
//            case Element.FIRE:
//                weakness.Add(Element.WIND);
//                weakness[0] = Element.EARTH;
//                break;
//            case Element.WATER:
//                weakness.Add(Element.FIRE);
//                weakness.Add(Element.ELECTRIC);
//                break;
//            case Element.EARTH:
//                weakness.Add(Element.WATER);
//                break;
//            case Element.WIND:
//                weakness.Add(Element.EARTH);
//                break;
//            case Element.ELECTRIC:
//                weakness[0] = Element.EARTH;
//                break;
//        }
//        return weakness;
//    }

//    //public bool IsWeaknessTo(Element myElem,Element enemyElement)
//    //{
//    //    return Weakness(myElem).Tolis
//    //}
//}
