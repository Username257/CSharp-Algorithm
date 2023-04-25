using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTreeAssignment
{
    internal class BinarySearchTree<T> where T : IComparable<T>
    {
        private class Node
        {
            internal T item; //내가 들고있는 아이템
            internal Node parent; //나의 부모
            internal Node left; //나의 왼쪽 자식
            internal Node right; //나의 오른쪽 자식
            //private이랑 프로퍼티 안 쓴 이유는?

            public Node(T item, Node parent, Node left, Node right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }

            //EraseNode를 위한 프로퍼티들
            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left == null && right != null; } }
            public bool HasBothChild { get { return left != null && right != null; } }
        }

        private Node root;
        //맨 첫 번재인 노드(필수적)

        public BinarySearchTree()
        {
            this.root = null;
        }

        public void Add(T item)
        {
            //루트가 없을 때 = 루트에다 추가
            //루트가 있을 때 - 루트보다 작으면 왼쪽에, 크면 오른쪽의 애를 루트로 가짐
            //루트보다 작으면, 크면 반복하다가 루트가 없으면 그 자리가 내 자리가 됨

            //노드를 만들어서 아이템을 집어넣고 그 노드를 뿌리에 두고, 자식에 두고 함
            Node newNode = new Node(item, null, null, null);

            //비교해야할 루트 노드가 계속 바뀔테니까 변수 생성 그리고 지금 루트(첫 번째 비교대상)주기
            Node currentNode = root;
            
            if (root == null)
            {
                root = newNode;
            }
            else //루트가 있을 때
            {
                //언제까지 반복하면 될까? 보니까 끝날 때 부모를 갖는 거 같음
                //라고 생각해봤읍니다 newNode.parent != null 이거 되나
                while (currentNode != null)
                {
                    if (item.CompareTo(currentNode.item) < 0) //얘가 root.item보다 작음
                    {
                        //왼쪽 루트를 갖자
                        //그 전에 왼쪽 자식이 있는 지 봐야 함
                        if (currentNode.left != null)
                            currentNode = currentNode.left;
                        else
                        {
                            currentNode.left = newNode;
                            //바꾸고 이어주기
                            newNode.parent = currentNode;
                            break;
                        }
                    }
                    else if (item.CompareTo(currentNode.item) > 0) //얘가 root.item보다 큼
                    {
                        //오른쪽 루트를 갖자 자식 있는 지도 보고
                        if (currentNode.right != null)
                            currentNode = currentNode.right;
                        else
                        {
                            currentNode.right = newNode;
                            newNode.parent = currentNode;
                            break;
                        }
                    }
                    else //같으면...? 그냥 끝? 중복 없으니까?ㅇㅇ
                        break;
                }
            }

        }

        public Node FindNode(T item) //왜 일관성 없다 하는데!!!!!!!!!!!
        {
            //루트보다 크면 큰쪽으로 가고.. 작으면 작은 쪽으로 가보고..
            //item갖고 값 비교하는 거 add랑 똑같지 않나?

            if (root == null) //아무것도 없을 경우
                return null;

            Node currentNode = root;

            while (currentNode != null)
            {
                if (item.CompareTo(currentNode.item) < 0)
                    currentNode = currentNode.left;

                else if (item.CompareTo(currentNode.item) > 0)
                    currentNode = currentNode.right;

                else
                    return currentNode;
            }
        }

        public bool Remove(T item)
        {
            //왠진 모르겠으나 EraseNode를 다른 함수로 빼놓음
            Node findNode = FindNode(item);
            if (findNode == null)
            {
                return false;
            }
            else
            {
                EraseNode(findNode);
                return true;
            }
        }
        public void EraseNode(Node eraseNode)
        {
            //왼쪽에서 가장 큰 애를 자기 자리에 주는 듯
            //그러면 자식이 있으면 게속 그 자식의 왼쪽으로 가다가 자식이 없으면 걔를 내 자리로
            //오른쪽 자식만 있는 경우엔 그냥 땡겨오면 됨(근데 어떻게?)
            Node currentNode = eraseNode; //현재 위치의 노드
            if (currentNode.left != null)
                currentNode = currentNode.left;
            else
                currentNode = currentNode.right;
        }

    }
}
