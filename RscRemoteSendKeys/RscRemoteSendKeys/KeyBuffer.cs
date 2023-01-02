using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RscRemoteSendKeys
{
    public class KeyBuffer
    {
        const int ciKEY_BUFFER_SIZE = 32; //100;

        protected KeyBufferItem[] m_aKeys = new KeyBufferItem[ciKEY_BUFFER_SIZE];

        protected int m_iIdxToDo = 0;
        protected int m_iIdxLast = 0;

        public KeyBuffer()
        {
            for (int i = 0; i < ciKEY_BUFFER_SIZE; i++)
            {
                m_aKeys[i] = new KeyBufferItem();
            }
        }

        protected bool MakeRoomToAdd()
        {
            if (m_aKeys[m_iIdxLast].bDone)
            {
                //NOP...
            }
            else
            {
                if (m_iIdxLast < (ciKEY_BUFFER_SIZE - 1) - 1)
                {
                    //if ((m_iIdxLast + 1) == m_iIdxToDo)
                    //    return false;

                    if (!m_aKeys[m_iIdxLast + 1].bDone)
                        return false;

                    m_iIdxLast++;
                }
                else
                {
                    //if (m_iIdxToDo == 0)
                    //    return false;

                    if (!m_aKeys[0].bDone)
                        return false;

                    m_iIdxLast = 0;
                }
            }

            return true;
        }

        public bool Add(char cKey)
        {
            if (!MakeRoomToAdd())
                return false;

            m_aKeys[m_iIdxLast].bDone = false;
            m_aKeys[m_iIdxLast].cKey = cKey;
            m_aKeys[m_iIdxLast].sKeyName = "";

            return true;
        }

        public bool Add(string sKey)
        {
            if (!MakeRoomToAdd())
                return false;

            m_aKeys[m_iIdxLast].bDone = false;
            m_aKeys[m_iIdxLast].cKey = '\0';
            m_aKeys[m_iIdxLast].sKeyName = sKey;

            return true;
        }

        public int GetToDoCount()
        {
            if (m_iIdxToDo == m_iIdxLast)
            {
                if (m_aKeys[m_iIdxLast].bDone)
                    return 0;

                return 1;
            }

            if (m_iIdxToDo < m_iIdxLast)
                return (m_iIdxLast - m_iIdxToDo);
            else
                return ((ciKEY_BUFFER_SIZE - 1) - m_iIdxToDo) + (m_iIdxLast + 1);
        }

        public bool IsFull()
        {
            return (GetToDoCount() == ciKEY_BUFFER_SIZE);
        }

        public KeyBufferItem GetToDoItem()
        {
            if (m_aKeys[m_iIdxToDo].bDone)
                return null;

            return m_aKeys[m_iIdxToDo];
        }

        public bool HasNextToDoItem()
        {
            if (m_iIdxToDo == (ciKEY_BUFFER_SIZE - 1))
            {
                if (!m_aKeys[0].bDone)
                {
                    return true;
                }
            }
            else
            {
                if (!m_aKeys[m_iIdxToDo + 1].bDone)
                {
                    return true;
                }
            }

            return false;
        }

        public void SetToDoItemDone()
        {
            m_aKeys[m_iIdxToDo].bDone = true;
            m_aKeys[m_iIdxToDo].cKey = '\0';
            m_aKeys[m_iIdxToDo].sKeyName = "";

            if (m_iIdxToDo == (ciKEY_BUFFER_SIZE - 1))
            {
                if (!m_aKeys[0].bDone)
                {
                    m_iIdxToDo = 0;
                }
            }
            else
            {
                if (!m_aKeys[m_iIdxToDo + 1].bDone)
                {
                    m_iIdxToDo++;
                }
            }
        }
    }

    public class KeyBufferItem
    {
        public bool bDone = true;
        public char cKey = '\0';
        public string sKeyName = "";

        public KeyBufferItem()
        {
            bDone = true;
            cKey = '\0';
            sKeyName = "";
        }
    }
}
