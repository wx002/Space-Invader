using System.Diagnostics;
using System;
using System.Xml;

namespace SpaceInvaders
{
    class GlyphManager: Manager
    {
        private readonly Glyph node;
        private static GlyphManager gManager = null;
        private static Glyph[] glyphArray = new Glyph[100];//using array, since it was indicated as legal for this in piazza @197_f2

        private GlyphManager(int reserveSize, int growthSize):base(new DList(), new DList(), reserveSize, growthSize)
        {
            node = new Glyph();
        }

        public static void CreateGlyphManager(int reserveSize, int growthSize)
        {
            Debug.Assert(reserveSize > 0);
            Debug.Assert(growthSize > 0);

            Debug.Assert(gManager == null);
            if (gManager == null)
            {
                // LTN - GlyphManaer
                gManager = new GlyphManager(reserveSize, growthSize);
            }
        }

        public void Destroy()
        {
            Debug.Assert(gManager != null);
            GlyphManager s = GetGlyphManager();
            s = null;
            Debug.Assert(gManager == null);
        }

        private static GlyphManager GetGlyphManager()
        {
            Debug.Assert(gManager != null);
            return gManager;
        }

        public static Glyph Add(Glyph.GlyphName name, int key, Texture.TextureName textureName, float x, float y, float width, float height)
        {
            GlyphManager gManager = GetGlyphManager();
            Debug.Assert(gManager != null);

            Glyph node = (Glyph)gManager.ManagerAddFront();
            Debug.Assert(node != null);

            node.Set(name, key, textureName, x, y, width, height);
            //add to array too
            return node;
        }

        public static Glyph Find(Glyph.GlyphName n, int key)
        {
            GlyphManager m = GetGlyphManager();
            Debug.Assert(m != null);

            m.node.name = n;
            m.node.key = key;
            Glyph found = (Glyph)m.ManagerFind(m.node);
            return found;
        }

        public static Glyph ArrayFind(int key)
        {
            return glyphArray[key - 32];
        }

        public static void Remove(Glyph g)
        {
            Debug.Assert(g != null);
            GlyphManager tMan = GetGlyphManager();
            Debug.Assert(tMan != null);
            tMan.ManagerRemove(g);
        }

        public static void LoadGlyphFromXML(Glyph.GlyphName name, string xmlFile, Texture.TextureName textureName)
        {
            XmlTextReader reader = new XmlTextReader(xmlFile);

            int key = -1;
            int x = -1;
            int y = -1;
            int width = -1;
            int height = -1;

            // parse the xml to grab the glyph visual images and store them into glyph manager
            // to build string, we grab each char using find 
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        if (reader.GetAttribute("key") != null)
                        {
                            key = Convert.ToInt32(reader.GetAttribute("key"));
                        }
                        else if (reader.Name == "x")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    x = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "y")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    y = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "width")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    width = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        else if (reader.Name == "height")
                        {
                            while (reader.Read())
                            {
                                if (reader.NodeType == XmlNodeType.Text)
                                {
                                    height = Convert.ToInt32(reader.Value);
                                    break;
                                }
                            }
                        }
                        break;

                    case XmlNodeType.EndElement: //Display the end of the element 
                        if (reader.Name == "character")
                        {
                            //Debug.WriteLine("key:{0} x:{1} y:{2} w:{3} h:{4}", key, x, y, width, height);
                            Glyph node = Add(name, key, textureName, x, y, width, height);
                            //start key = 32, so key - 32 would normalize the indexing
                            glyphArray[key - 32] = node;
                        }
                        break;
                }
            }
        }


        protected override bool Compare(NodeBase nodeA, NodeBase nodeB)
        {
            Debug.Assert(nodeA != null);
            Debug.Assert(nodeB != null);
            Glyph tA = (Glyph)nodeA;
            Glyph tB = (Glyph)nodeB;
            bool cmp = false;
            if (tA.name == tB.name && tA.key == tB.key)
            {
                cmp = true;
            }
            return cmp;
        }

        protected override NodeBase CreateNode()
        {
            // LTN - TextureManager
            NodeBase n = new Glyph();
            Debug.Assert(n != null);
            return n;
        }
    }
}
