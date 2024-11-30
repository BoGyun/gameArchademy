using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _inst;
    public static T Inst
    {
        get
        {
            if (_inst == null)
            {
                _inst = FindObjectOfType<T>();

                if (_inst == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    _inst = obj.AddComponent<T>();
                }
            }
            return _inst;
        }
    }

    private void Awake()
    {
        // �ߺ� ����: �̹� �ν��Ͻ��� �����ϸ� ���� ������ ������Ʈ�� �ı�
        if (_inst == null)
        {
            _inst = this as T;
        }
        else if (_inst != this)
        {
            Destroy(gameObject);
        }
    }
}

public class PoolingObject
{
    public GameObject Instance { get; set; }
}

public class Bullet : PoolingObject 
{
    public Rigidbody Rigidbody { get; set; }
}



    public class ObjectPool : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    private Queue<GameObject> pool = new Queue<GameObject>();

    public void SetPool(GameObject Obj, int Size)
    {
        for(int i = 0; i < Size; i++) 
        {
            GameObject obj = Instantiate(Obj);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }

    }

    public GameObject GetObject(float returnTime = 0)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
        }
        else
            obj = Instantiate(prefab);


        if (returnTime != 0)
            StartCoroutine(ReserveReturn(obj, returnTime));


        return obj;
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }

    private IEnumerator ReserveReturn(GameObject obj, float time)
    {
        yield return new WaitForSeconds(time);

        ReturnObject(obj);
    }


}
