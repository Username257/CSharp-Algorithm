using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

            //EraseNode를 위한 프로퍼티들(왜 프로퍼티로 했을까)
            /*
            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }

            public bool HasNoChild { get { return left == null && right == null; } }
            public bool HasLeftChild { get { return left != null && right == null; } }
            public bool HasRightChild { get { return left == null && right != null; } }
            public bool HasBothChild { get { return left != null && right != null; } }
            */
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

            return null;
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
        public void EraseNode(Node eraseNode) //얘도 일관성 없다 하네
        {
            
            //1. eraseNode가 왼쪽 자식이다 == 부모 노드와 왼쪽 자식 관계를 끊는다
            //2. 오른쪽 자식이다 == 부모 노드와 오른쪽 자식 관계를 끊는다
            //3. root다 == 자기 자신을 지운다(root 하나만 있는 상태다)

            bool IsLeftChild = (eraseNode.parent != null && eraseNode.parent.left == eraseNode) ? true : false;
            bool IsRightChild = (eraseNode.parent != null && eraseNode.parent.right == eraseNode) ? true : false;
            

            //는 자식 노드가 없는 경우고
            if (eraseNode.right == null && eraseNode.left == null)
            {
                if (IsLeftChild)
                    eraseNode.parent.left = null;
                else if (IsRightChild)
                    eraseNode.parent.right = null;
                else
                    eraseNode = null;
            }

            //자식 노드가 있는 경우..
            //왼쪽에서 가장 큰 애를 자기 자리에 주는 듯 <- 그냥 자기 왼쪽 자식을 자신으로 대체한다는 말이 되는 듯
            //오른쪽 자식만 있는 경우엔 그냥 땡겨오면 됨(근데 어떻게? -> 그냥 연결해주면 됨)

            bool HasLeftChild = (eraseNode.left != null && eraseNode.right == null) ? true : false;
            bool HasRightChild = (eraseNode.left == null && eraseNode.right != null) ? true : false;
            bool HasBothchild = (eraseNode.left != null && eraseNode.right != null) ? true : false;
            
            if (eraseNode.right != null && eraseNode.left == null)
            {
                eraseNode.parent.right = eraseNode.right;
                eraseNode.right.parent = eraseNode.parent;
            } 
            
            if(eraseNode.right == null && eraseNode.left != null)
            {
                eraseNode.parent.left = eraseNode.left;
                eraseNode.left.parent = eraseNode.parent;
            }

            if (eraseNode.right != null && eraseNode.left != null)
            {
                Node replaceNode = eraseNode.left;
                while (replaceNode.right != null)
                {
                    replaceNode = replaceNode.right; //제일 큰 값을 계속 준다
                }
                eraseNode.item = replaceNode.item; //제일 큰 값을 받아온다
                EraseNode(replaceNode); //를 재귀함수로 반복
            }
        }


        public bool TryGetValue(T item, out T outValue)
        {
            /*
              루트가 없다면 == false리턴, outValue에 디폴트
              최근 노드에 루트 주고
              반복문 {
              찾으려는 아이템이 최근 노드의 값보다 작으면 왼쪽 자식을 최근 노드에 줌
              크면 오른쪽 자식을 주고
              같으면 그 outValue에 그 값 주고 true }
              반복문을 나왔다(무슨 경우지?) == false, 디폴트
            */
            //아이템을 찾으면 true, 못 찾으면 false고
            //out 이랑 outValue는 뭐지?
            //out 키워드를 사용하면 변수가 참조로 전달 됨
            //out 키워드를 사용한 매개변수는 함수 내부에서 무조건 값을 세팅해줘야함
            //ref는 사용하기 전에 초기화, out은 초기화 필요 없음

            //함수를 쓸 때 (찾을 아이템, 함수 내에서 준 outValue받을 변수) 로 매개변수 주는 거 같고
            //그니까.. 그냥 맨 값을 입력하면 실제 트리 안에 있는 노드의 값을 준다는 듯?

            if (root == null)
            {
                outValue = default(T);
                return false;
            }
            else
            {
                Node currentNode = root;
                while(currentNode != null)
                {
                    if (item.CompareTo(currentNode.item) < 0) //왜 T에는 CompareTo 해야하는 지 <- 인터페이스가 비교연산자 지원 안 해준다는 듯, null일 수도 있으니
                        currentNode = currentNode.left;
                    else if (item.CompareTo(currentNode.item) > 0)
                        currentNode = currentNode.right;
                    else
                    {
                        outValue = currentNode.item;
                        return true;
                    }
                }
                outValue = default(T);
                return false;
            }
        }
    }
}

