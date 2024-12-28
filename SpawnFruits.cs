
using System.Collections.Generic;
using UnityEngine;
using static SpawnFruits;

public class SpawnFruits : MonoBehaviour
{

    private static Transform pStart;
    private static Transform pEnd;
    public class Fruit
    {
        public string fruitName;

        public Fruit(string fruitName)
        {
            this.fruitName = fruitName;
        }

        public GameObject CreateFruit(Vector3 fPosition, Quaternion qQ)
        {
            GameObject fPrefab = Resources.Load<GameObject>(fruitName);
            return Instantiate(fPrefab, fPosition, qQ);
        }
    }

    public class FruitFactory
    {
        public static Fruit CreateFruit(string fruitType)
        {
            return new Fruit(fruitType);  // ͨ������ˮ����������̬����ˮ��ʵ��
        }
    }


    //fruit���� ������Ӳ�ͬˮ��
    //public class Fruit
    //{
        
    //    public virtual GameObject CreateFruit(Vector3 fPosition, Quaternion qQ)
    //    {
    //        return null;
    //    }
    //}

    //public class Apple : Fruit
    //{
    //    public override GameObject CreateFruit(Vector3 fPosition, Quaternion qQ)
    //    {
    //        GameObject fPrefab = Resources.Load<GameObject>("apple");
    //        return Instantiate(fPrefab,fPosition,qQ);
    //    }
    //}
    //public class Banana : Fruit
    //{
    //    public override GameObject CreateFruit(Vector3 fPosition, Quaternion qQ)
    //    {
    //        GameObject fPrefab = Resources.Load<GameObject>("banana");
    //        return Instantiate(fPrefab, fPosition, qQ);
    //    }
    //}
    //public class Cherries : Fruit
    //{
    //    public override GameObject CreateFruit(Vector3 fPosition, Quaternion qQ)
    //    {
    //        GameObject fPrefab = Resources.Load<GameObject>("cherries");
    //        return Instantiate(fPrefab, fPosition, qQ);
    //    }
    //}


    //���Թ���
    
    public abstract class CreateStrategy
    {
        public abstract Fruit ChooseFruit();
    }


    public class AppleStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("apple");
        }
    }
    public class BananaStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("banana");
        }
    }
    public class CherriesStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("cherries");
        }
    }
    public class LemonsStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("lemon");
        }
    }
    public class PeachesStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("peach");
        }
    }
    public class StrawberryStrategy : CreateStrategy
    {
        public override Fruit ChooseFruit()
        {
            return FruitFactory.CreateFruit("strawberry");
        }
    }

    class RandomFruitSrategy : CreateStrategy
    {
        protected List<CreateStrategy> strategies = new List<CreateStrategy>();


        public RandomFruitSrategy()
        {
            strategies.Add(new AppleStrategy());
            strategies.Add(new BananaStrategy());
            strategies.Add(new CherriesStrategy());
            strategies.Add(new LemonsStrategy());
            strategies.Add(new PeachesStrategy());
            strategies.Add(new StrawberryStrategy());
        }


        public override Fruit ChooseFruit()
        {
            int index = UnityEngine.Random.Range(0,strategies.Count);
            return strategies[index].ChooseFruit();
        }

    }
    
    public class FruitStrategyContext
    {
        private CreateStrategy createStrategy;

        public FruitStrategyContext(CreateStrategy createStrategy)
        {
            this.createStrategy = createStrategy;
        }

        public Fruit ExecuteStrategy()
        {
            return createStrategy.ChooseFruit();
        }
    }



    public interface FInstance  //װ��������
    {
        void ChangePosition(GameObject obj, Vector3 vPos);
    }

    public class ImplementsFInstance : FInstance
    {
        public GameObject prefab;
        public ImplementsFInstance(GameObject fPrefab)
        {
            this.prefab = fPrefab;
        }

        public void ChangePosition(GameObject obj, Vector3 vPos)
        {
        }
    }

    public abstract class FInstanceDecorator : FInstance
    {
        protected FInstance fInstance;

        public FInstanceDecorator(FInstance fInstance)
        {
            this.fInstance = fInstance;
        }

        public abstract void ChangePosition(GameObject obj, Vector3 vPos);
    }

    public class FInstanceRandomPosDecorator : FInstanceDecorator
    {
        public FInstanceRandomPosDecorator(FInstance fInstance) : base(fInstance)
        {
        }

        public override void ChangePosition(GameObject obj,Vector3 vPos)
        {
            //����ʵ��
            obj.transform.position = vPos;
        }
    }


    //����ˮ���Ĳ��ԣ����岻ͬ������ˮ���Ĳ��ԣ���update�ж�ʱ���ò�ͬ�Ĳ���������ˮ��
    public abstract class FruitInstatiateStrategyFactory
    {
        public abstract FruitInstatiateStrategyFactory Instantiate();
    }

    public class LessFruitStrategy : FruitInstatiateStrategyFactory
    {
        public override FruitInstatiateStrategyFactory Instantiate()
        {

            int times = Random.Range(0,3);

            TypeASpawn typeASpawn = new TypeASpawn();

            for (int i = 0; i < times; i++)
            {
                GameObject instance = typeASpawn.DoStart();
                typeASpawn.DoDecorator();
                typeASpawn.DoForce();
            }
            Debug.Log("���������ٵ���ϣ�" + times + "��");
            return new LessFruitStrategy();
        }
    }
    public class NormalFruitStrategy : FruitInstatiateStrategyFactory
    {
        public override FruitInstatiateStrategyFactory Instantiate()
        {
            int times = Random.Range(2, 5);

            TypeASpawn typeASpawn = new TypeASpawn();

            for (int i = 0; i < times; i++)
            {
                GameObject instance = typeASpawn.DoStart();
                typeASpawn.DoDecorator();
                typeASpawn.DoForce();
            }
            Debug.Log("������һ������" + times + "��");
            return new NormalFruitStrategy();
        }
    }
    public class MoreFruitStrategy : FruitInstatiateStrategyFactory
    {
        public override FruitInstatiateStrategyFactory Instantiate()
        {
            int times = Random.Range(4, 7);

            TypeASpawn typeASpawn = new TypeASpawn();

            for (int i = 0; i < times; i++)
            {
                GameObject instance = typeASpawn.DoStart();
                typeASpawn.DoDecorator();
                typeASpawn.DoForce();
            }
            Debug.Log("�����˸�������" + times + "��");
            return new MoreFruitStrategy();
        }
    }

    public class RandomFruitInstatiateStrategyFactoryStrategies : FruitInstatiateStrategyFactory
    {
        List<FruitInstatiateStrategyFactory> m_fruitInstatiateStrategyFactories;
        public RandomFruitInstatiateStrategyFactoryStrategies(List<FruitInstatiateStrategyFactory> fruitInstatiateStrategyFactories)
        {
            this.m_fruitInstatiateStrategyFactories = fruitInstatiateStrategyFactories;
        }

        public override FruitInstatiateStrategyFactory Instantiate()
        {
            int index = Random.Range(0, m_fruitInstatiateStrategyFactories.Count);
            return m_fruitInstatiateStrategyFactories[index].Instantiate();
        }
    }

    public class FruitInstatiateStrategyFactoryContext
    {
        private FruitInstatiateStrategyFactory m_strategy;

        public FruitInstatiateStrategyFactoryContext(FruitInstatiateStrategyFactory strategy)
        {
            this.m_strategy = strategy;
        }

        public FruitInstatiateStrategyFactory ExecuteStrategy()
        {
            return m_strategy.Instantiate();
        }
    }


    public class SpawnTemplate
    {
        public virtual GameObject DoStart()
        {
            return null;
        }

        public virtual void DoDecorator()
        {

        }

        public virtual void DoForce()  //��������
        {

        }
    }


    public static List<GameObject> spawnedFruits = new List<GameObject>();
    public class TypeASpawn : SpawnTemplate
    {
        private GameObject m_fInstance;

        public override GameObject DoStart()
        {
            FruitStrategyContext fruitStrategyContext = new FruitStrategyContext(new RandomFruitSrategy());  //����������

            Fruit fruit = fruitStrategyContext.ExecuteStrategy();  //ִ�в���
            m_fInstance = fruit.CreateFruit(Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0, 0)));  //create

            //��ӽ���������ˮ�����б�
            spawnedFruits.Add(m_fInstance);

            return m_fInstance;
        }

        public override void DoDecorator()
        {
            ImplementsFInstance fInstance1 = new ImplementsFInstance(m_fInstance);
            FInstanceRandomPosDecorator fInstance2 = new FInstanceRandomPosDecorator(fInstance1);
            Vector3 rPos = new Vector3(Random.Range(pStart.position.x, pEnd.position.x),  //x
                0,  //y
                5.41f);  //z
            fInstance2.ChangePosition(m_fInstance, rPos);

        }

        public override void DoForce()  //��������
        {
            Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 5.41f));
            float horizontalForce = Random.Range(2, -2f);
            float verticalForce = Random.Range(2, -2);
            GenerateForce(m_fInstance.GetComponent<Rigidbody>(), new Vector3(0, topLeft.y * 1.5f, Random.Range(0, -4)), horizontalForce, verticalForce);
        }

        private void GenerateForce(Rigidbody rb, Vector3 tar, float horizontalRVal, float verticalRVal)
        {
            //ʩ���� ��������
            rb.AddForce(tar + new Vector3(horizontalRVal, verticalRVal, 0), ForceMode.Impulse);

            // ʹ������Ȼ��ת�����ý��ٶ�
            rb.angularVelocity = new Vector3(Random.Range(10f, 50f), Random.Range(10f, 50f), Random.Range(10f, 50f));  // ������Y����ת
        }
    
    }


    //public void DoSpawn()
    //{

    //    FruitStrategyContext fruitStrategyContext = new FruitStrategyContext(new RandomFruitSrategy());  //����������

    //    Fruit fruit = fruitStrategyContext.ExecuteStrategy();  //ִ�в���
    //    GameObject fInstance = fruit.CreateFruit(Vector3.zero, Quaternion.Euler(new Vector3(-90f, 0, 0)));  //create

    //    ImplementsFInstance fInstance1 = new ImplementsFInstance(fInstance);
    //    FInstanceRandomPosDecorator fInstance2 = new FInstanceRandomPosDecorator(fInstance1);
    //    Vector3 rPos = new Vector3(Random.Range(pStart.position.x, pEnd.position.x),  //x
    //        0,  //y
    //        5.41f);  //z
    //    fInstance2.ChangePosition(fInstance, rPos);

    //    Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 5.41f));
    //    float horizontalForce = Random.Range(2, -2f);
    //    float verticalForce = Random.Range(2, -2);
    //    GenerateForce(fInstance.GetComponent<Rigidbody>(), new Vector3(0, topLeft.y * 1.5f, Random.Range(0, -4)), horizontalForce, verticalForce);

    //}



    private void Awake()
    {
        pStart = GameObject.Find("P1").transform;
        pEnd = GameObject.Find("P2").transform;
    }

    private void Start()
    {
        StartCoroutine(DetectAndDeleteFruitBelowY());
    }

    System.Collections.IEnumerator DetectAndDeleteFruitBelowY()
    {
        while(true)
        {
            yield return new WaitForSeconds(1);

            if(spawnedFruits.Count > 0 )
            {
                for (int i = spawnedFruits.Count - 1; i >= 0; i--)
                {
                    GameObject go = spawnedFruits[i];
                    if (go != null)
                    {
                        if (go?.transform?.position.y <= -1)
                        {
                            if (go != null)
                            {
                                Destroy(go);
                                spawnedFruits.RemoveAt(i);
                            }
                        }
                    }

                    if(go == null)
                    {
                        spawnedFruits.RemoveAt(i);
                    }
                }
            }
        }
    }

    public float timer = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        if(timer > Random.Range(1.1f,4.1f))
        {
            LessFruitStrategy lessStr = new LessFruitStrategy();
            NormalFruitStrategy normalStr = new NormalFruitStrategy();
            MoreFruitStrategy moreStr = new MoreFruitStrategy();
            List<FruitInstatiateStrategyFactory> fisFactories = new List<FruitInstatiateStrategyFactory>();
            fisFactories.Add(lessStr);
            fisFactories.Add(normalStr);
            fisFactories.Add(moreStr);

            FruitInstatiateStrategyFactoryContext fisfContext = new FruitInstatiateStrategyFactoryContext(new RandomFruitInstatiateStrategyFactoryStrategies(fisFactories));
            FruitInstatiateStrategyFactory fisFactory = fisfContext.ExecuteStrategy();
            timer = 0;
        }

    }
}
