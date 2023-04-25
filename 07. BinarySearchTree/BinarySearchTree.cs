using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class BinarySearchTree<T> where T : IComparable<T> //비교 가능한 데이터만 들어와서 사용 가능
    {
        //노드가 필요하다 이진탐색트리는 완전 이진 트리를 보장X
        //새로운 데이터 추가하려면 마지막에 두는 게 아니라 자신의 위치에 감(중간이 비어있음)

        private Node root;
        //가장 위의 노드 루트부터 작은 경우 / 큰 경우로 찾으니까

        public BinarySearchTree()
        {
            this.root = null; //처음엔 없어야하니까
        }

        public void Add(T item)
        {
            //c#의 경우는 추가 시 true, 아닐 시 false의 bool 반환형임
            Node newNode = new Node(item, null, null, null);

            if(root == null)
            {
                root = newNode;
                return;
            }

            Node current = root;
            while(current != null)
            {
                //비교해서 더 작은 경우 왼쪽으로 감
                if (item.CompareTo(current.item) < 0)
                {
                    //비교노드가 왼쪽 자식이 있는 경우
                    if(current.left != null)
                    {
                        //왼쪽 자식과 또 비교하기 위해 current 왼쪽자식으로 설정
                        current = current.left;
                    }
                    //비교노드가 왼쪽 자식이 없는 경우
                    else
                    {
                        //그 자리가 지금 추가가 될 자리
                        current.left = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                //비교해서 더 큰 경우 오른쪽으로 감
                else if(item.CompareTo(current.item) > 0)
                {
                    //비교 노드가 오른쪽 자식이 있는 경우
                    if(current.right != null)
                    {
                        //오른쪽 자식과 또 비교하기 위해 current를 오른쪽 자식으로 설정
                        current = current.right;
                    }
                    //비교 노드가 오른쪽 자식이 없는 경우
                    else
                    {
                        //그 자리가 지금 추가가 될 자리
                        current.right = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                //동일한 데이터가 들어온 경우
                else
                {
                    return; //아무것도 안하기
                }
            }
        }

        public bool Remove(T item)
        {
            //제거 됐으면 true
            Node findNode = FindNode(item);
            if(findNode == null)
            {
                return false;
            }
            else
            {
                EraseNode(findNode);
                return true;
            }
        }
       
        private Node FindNode(T item)
        {
            if (root == null)
                return null;

            Node current = root;
            while (current != null)
            {
                if (item.CompareTo(current.item) < 0)
                {
                    current = current.left;
                }
                else if (item.CompareTo(current.item) > 0)
                {
                    current = current.right;
                }
                else
                {
                    return current;
                }
            }

            return null;
        }
        public bool TryGetValue(T item, out T outValue)
        {
            //찾았으면 true
            if(root == null)
            {
                outValue = default(T);
                return false;
            }
            Node current = root;
            while(current != null)
            {
                //현재 노드의 값이 찾고자 하는 값보다 작은 경우
                if(item.CompareTo(current.item) < 0)
                {
                    //왼쪽 자식부터 다시 찾기 시작
                    current = current.left;
                }
                //현재 노드의 값이 찾고자 하는 값보다 큰 경우
                else if(item.CompareTo(current.item) > 0)
                {
                    //오른쪽 자식부터 다시 찾기 시작
                    current = current.right;
                }
                //현재 노드의 값이 찾고자 하는 값이랑 같은 경우
                else
                {
                    //찾음
                    outValue = current.item;
                    return true;
                }
            }
            outValue = default(T); //값형식은 데이터가 없다를 할 수 없다
            return false;
        }
        private void EraseNode(Node node)
        {
            //1. 자식 노드가 없는 노드일 경우
            if (node.left == null && node.right == null)
            {
                if (node.IsLeftChild)
                    node.parent.left = null;
                else if (node.IsRightChild)
                    node.parent.right = null;
                else //if (node.IsRootNode)
                    root = null;
            }
            //2. 자식 노드가 1개인 노드일 경우
            else if (node.HasLeftChild || node.HasRightChild)
            {
                Node parent = node.parent;
                Node child = node.HasLeftChild ? node.left : node.right;

                if (node.IsLeftChild)
                {
                    parent.left = child;
                    child.parent = parent;
                }
                else if (node.IsRightChild)
                {
                    parent.right = child;
                    child.parent = parent;
                }
                else
                {
                    root = child;
                    child.parent = null;
                }
                
            }
            //3. 자식 노드가 2개인 노드일 경우
            //왼쪽 자식 중 가장 큰 값과 데이터 교환한 수, 그 노드를 지워주는 방식으로 대체
            else //if (node.HasBothChild)
            {
                Node replaceNode = node.left;
                while(replaceNode.right != null)
                {
                    replaceNode = replaceNode.right;
                }
                node.item = replaceNode.item;
                EraseNode(replaceNode);

                /* 둘이 같음
                replaceNode = node.eight;
                while (replaceNode.left != null)
                {
                    replaceNode = replaceNode.left;
                }
                node.item = replaceNode.item;
                EraseNode(replaceNode);
                */
            }
        }
        public class Node
        {
            internal T item;
            internal Node left;
            internal Node right;
            internal Node parent;

            public Node (T item, Node parent, Node left, Node right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }
            

            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left == null && right != null; } }
            public bool HasBothChild { get { return left != null && right != null; } }
        }
    }
}
