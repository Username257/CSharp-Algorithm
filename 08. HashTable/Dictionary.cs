using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _08._HashTable
{
    //IComparable은 순서, IEquatable은 값이 같은 지
    public class Dictionary<TKey, TValue> where TKey : IEquatable<TKey> //비교할 수 있어야함
    {
        private const int DefaultCapacity = 1000; //용량을 포기하고 성능을 택한다

        private struct Entry
        {
            public enum State { None, Using, Deleted }

            public State state; 
            public int hashCode; 
            
            public TKey key;
            public TValue value;
        }

        private Entry[] table;
        
        public Dictionary()
        {
            table = new Entry[DefaultCapacity];
        }
        public TValue this[TKey key]
        {
            get
            {
                //1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                //2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    //3-1. 동일한 키값을 찾았을 때 반환하기
                    if (key.Equals(table[index].key))
                    {
                        return table[index].value;

                    }
                    //3-2. 동일한 키값을 못찾고 비어있는 공간을 만났을 때
                    if (table[index].state != Entry.State.None)
                    {
                        break;
                    }
                    //3-3. 다음 index로 이동
                    index = index < table.Length ? index + 1 : 0;
                    
                }
                throw new InvalidOperationException();
            }
            set //덮어쓰기
            {
                //1. key를 index로 해싱
                int index = Math.Abs(key.GetHashCode() % table.Length);

                //2. key가 일치하는 데이터가 나올때까지 다음으로 이동
                while (table[index].state == Entry.State.Using)
                {
                    //3.동일한 키값을 찾았을 때 덮어쓰기
                    if (key.Equals(table[index].key))
                    {
                        table[index].value = value;
                        return;

                    }
                    if (table[index].state != Entry.State.None)
                    {
                        break;
                    }

                    index = ++index % table.Length;

                }
                throw new InvalidOperationException();
            }
        }

        public void Add(TKey key, TValue value)
        {
            //1. 키를 인덱스로 해싱
            //해싱 방법들 ~
            //나눗셈 법 - 키가 int일 때, 1119일때 앞에 1을 날리기
            //각 자릿수 더하기(자릿수 접기) - int일 떄, 문자열일 때(char 코드로 int 변환)
            int index = Math.Abs(key.GetHashCode() % table.Length); //어떤 자료형이든 해싱해주는 함수를 지원해줌
            //왜 지원 해주냐 - 모든 숫자에 대응하는 배열을 만드는 것은 비효율적임
            //해싱할 때 22222를 222에 저장하고, 1222도 222로 해싱할 수 있음(만능이 아니다)
            //SHA 1, SHA 256.. 암호화..

            //2.사용중이 아닌 index까지 다음으로 이동
            while (table[index].state == Entry.State.Using)
            {
                //2-1. 동일한 키 값을 찾았을 때 오류(C# 딕셔너리는 중복 X)
                if (key.Equals(table[index].key))
                {
                    throw new Exception();
                }
                //3-2. 다음 index로 이동(선형 탐사)
                index = ++index % table.Length;
            }

            //4. 사용중이 아닌 index를 발견한 경우 그 위치에 저장
            table[index].key = key;
            table[index].value = value;
            table[index].state = Entry.State.Using;
            
        }

        public bool Remove(TKey key)
        {
            //1. key를 index로 해싱
            int index = Math.Abs(key.GetHashCode() % table.Length);

            //2. 키 값과 동일한 데이터를 찾을 때까지 idex 증가
            while (table[index].state == Entry.State.Using)
            {
                
                if (key.Equals(table[index].key))
                {
                    table[index].state = Entry.State.Deleted;
                    return true;
                }
                if (table[index].state != Entry.State.None)
                {
                    break;
                }

                index = ++index % table.Length;

            }
            return false;
        }
    }
}
