using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ToolLibrarySystem
{
    public class MemberCollection : iMemberCollection
    {
        //class for Nodes in the Member Binary Tree
        private class MemberNode
        {
            private Member member;
            private MemberNode lchild;
            private MemberNode rchild;

            public MemberNode(Member member)
            {
                this.member = member;
                lchild = null;
                rchild = null;
            }

            public Member Member
            {
                get { return member; }
                set { member = value; }
            }

            public MemberNode LChild
            {
                get { return lchild; }
                set { lchild = value; }
            }

            public MemberNode RChild
            {
                get { return rchild; }
                set { rchild = value; }
            }
        }
        private int number;
        private MemberNode root;

        public MemberCollection()
        {
            root = null;
            number = 0;
        }

        public int Number
        { get { return number; } }

        public void add(Member member)
        {
            if (root == null)
            {
                root = new MemberNode(member);
                number++;
            }
            else
            {
                add(member, root);
                number++;
            }
        }

        private void add(Member member, MemberNode ptr)
        {
            if (member.CompareTo(ptr.Member) < 0)
            {
                if (ptr.LChild == null)
                {
                    ptr.LChild = new MemberNode(member);
                }
                else
                {
                    add(member, ptr.LChild);
                }
            }
            else
            {
                if (ptr.RChild == null)
                {
                    ptr.RChild = new MemberNode(member);
                }
                else
                {
                    add(member, ptr.RChild);
                }
            }
        }

        public void delete(Member member)
        {
            // search for item and its parent
            MemberNode ptr = root; // search reference
            MemberNode parent = null; // parent of ptr
            while ((ptr != null) && (member.CompareTo(ptr.Member) != 0))
            {
                parent = ptr;
                if (member.CompareTo(ptr.Member) < 0) // move to the left child of ptr
                {
                    ptr = ptr.LChild;
                }
                else
                {
                    ptr = ptr.RChild;
                }
            }

            if (ptr != null) // if the search was successful
            {
                // case 3: item has two children
                if ((ptr.LChild != null) && (ptr.RChild != null))
                {
                    // find the right-most node in left subtree of ptr
                    if (ptr.LChild.RChild == null) // a special case: the right subtree of ptr.LChild is empty
                    {
                        ptr.Member = ptr.LChild.Member;
                        ptr.LChild = ptr.LChild.LChild;
                    }
                    else
                    {
                        MemberNode p = ptr.LChild;
                        MemberNode pp = ptr; // parent of p
                        while (p.RChild != null)
                        {
                            pp = p;
                            p = p.RChild;
                        }
                        // copy the item at p to ptr
                        ptr.Member = p.Member;
                        pp.RChild = p.LChild;
                    }
                }
                else // cases 1 & 2: item has no or only one child
                {
                    MemberNode c;
                    if (ptr.LChild != null)
                    {

                        c = ptr.LChild;
                    }
                    else
                    {
                        c = ptr.RChild;
                    }

                    // remove node ptr
                    if (ptr == root) //need to change root
                    {
                        root = c;
                    }
                    else
                    {
                        if (ptr == parent.LChild)
                        {
                            parent.LChild = c;
                        }
                        else
                        {
                            parent.RChild = c;
                        }
                    }
                }
                number--;
            }
        }

        public bool search(Member member)
        {
            return search(member, root);
        }

        private bool search(Member member, MemberNode r)
        {
            if (r != null)
            {
                if (member.CompareTo(r.Member) == 0)
                {
                    return true;
                }
                else
                {
                    if (member.CompareTo(r.Member) < 0)
                    {
                        return search(member, r.LChild);
                    }
                    else
                    {
                        return search(member, r.RChild);
                    }
                }
            }
            else
                return false;
        }

        public Member[] toArray()
        {
            Member[] memberList = new Member[Number];
            int i = 0;
            PreOrderTraverse(root, ref memberList, ref i);
            return memberList;
        }

        private void PreOrderTraverse(MemberNode root, ref Member[] memberList, ref int i)
        {
            if (root != null)
            {
                memberList[i] = root.Member;
                i++;
                PreOrderTraverse(root.LChild, ref memberList, ref i);
                PreOrderTraverse(root.RChild, ref memberList, ref i);
            }
        }
    }
}
