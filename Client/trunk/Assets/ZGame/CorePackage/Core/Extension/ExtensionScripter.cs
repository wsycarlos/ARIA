
using CLRSharp;
using UnityEngine;

namespace Game.Core
{
    public class ExtensionScripter : MonoBehaviour
    {
        public static string extensionName;
        protected string _extensionName;
        private ICLRType _wantType;
        private ThreadContext _context;
        protected CLRSharp_Environment env;
        protected object methodctor;

        public virtual void Awake()
        {
            if (!string.IsNullOrEmpty(extensionName))
            {
                _extensionName = extensionName;
            }

            env = ExtensionManager.instance.env;
            if (null == env)
            {
                D.error("CLRSharp_Environment init failed");
                return;
            }
            _context = new ThreadContext(env);
            _wantType = env.GetType(_extensionName);

            if (null == _wantType)
            {
                D.error("Can not get env type of " + _extensionName);
                return;
            }

            Init();
            MemberCall("Awake", methodctor);
        }

        public virtual void Start()
        {
            MemberCall("Start", methodctor);
        }

        private void Init()
        {
            //取得构造函数 执行构造函数
            methodctor = MemberCall(".ctor", null);
            MemberCall("Init", methodctor, MethodParamList.Make(env.GetType(typeof(GameObject))), new object[] { gameObject });
        }

        public virtual void OnDestroy()
        {
            MemberCall("OnDestroy", methodctor);
        }

        public virtual void OnDisable()
        {
            MemberCall("OnDisable", methodctor);
        }

        public virtual void OnEnable()
        {
            MemberCall("OnEnable", methodctor);
        }

        /// <summary>
        /// 执行函数调用
        /// </summary>
        /// <param name="methodName">需要调用的外部类的方法名</param>
        /// <param name="ctor">构造函数，静态函数为null</param>
        /// <param name="list">方法参数类型</param>
        /// <param name="_parmas">方法参数表</param>
        /// <returns></returns>
        protected object MemberCall(string methodName, object ctor, MethodParamList list = null, object[] _parmas = null)
        {
            if (null == list)
            {
                list = MethodParamList.constEmpty();
            }

            IMethod method = _wantType.GetMethod(methodName, list);

            if (null == method)
            {
                return null;
            }

            return method.Invoke(_context, ctor, _parmas);
        }

        //void OnBecameInvisible()
        //{
        //    MemberCall("OnBecameInvisible", methodctor);
        //}
        //void OnBecameVisible()
        //{
        //    MemberCall("OnBecameVisible", methodctor);
        //}
        //void OnGUI()
        //{
        //    MemberCall("OnGUI", methodctor);
        //}

        //void OnLevelWasLoaded(int level)
        //{
        //List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //param.Add(new CSLE.CLS_Content.Value());
        //param[0].type = typeof(int);
        //param[0].value = level;
        //MemberCall("OnLevelWasLoaded", param);
        //}

        //void OnCollisionEnter(Collision col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision);
        //    param[0].value = col;
        //    MemberCall("OnCollisionEnter", param);
        //}
        //void OnCollisionEnter2D(Collision2D col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision2D);
        //    param[0].value = col;
        //    MemberCall("OnCollisionEnter2D", param);
        //}
        //void OnCollisionExit(Collision col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision);
        //    param[0].value = col;
        //    MemberCall("OnCollisionExit", param);
        //}
        //void OnCollisionExit2D(Collision2D col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision2D);
        //    param[0].value = col;
        //    MemberCall("OnCollisionExit2D", param);
        //}
        //void OnCollisionStay(Collision col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision);
        //    param[0].value = col;
        //    MemberCall("OnCollisionStay", param);
        //}
        //void OnCollisionStay2D(Collision2D col)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collision2D);
        //    param[0].value = col;
        //    MemberCall("OnCollisionStay2D", param);
        //}

        //void OnBecameInvisible()
        //{
        //    MemberCall("OnBecameInvisible");
        //}
        //void OnBecameVisible()
        //{
        //    MemberCall("OnBecameVisible");
        //}
        //void OnGUI()
        //{
        //    MemberCall("OnGUI");
        //}
        //void OnLevelWasLoaded(int level)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(int);
        //    param[0].value = level;
        //    MemberCall("OnLevelWasLoaded", param);
        //}


        //void OnMouseDown()
        //{
        //    MemberCall("OnMouseDown");
        //}
        //void OnMouseDrag()
        //{
        //    MemberCall("OnMouseDrag");
        //}
        //void OnMouseEnter()
        //{
        //    MemberCall("OnMouseEnter");
        //}
        //void OnMouseExit()
        //{
        //    MemberCall("OnMouseExit");
        //}
        //void OnMouseOver()
        //{
        //    MemberCall("OnMouseOver");
        //}
        //void OnMouseUp()
        //{
        //    MemberCall("OnMouseUp");
        //}
        //void OnMouseUpAsButton()
        //{
        //    MemberCall("OnMouseUpAsButton");
        //}

        //void OnMouseDown()
        //{
        //    MemberCall("OnMouseDown");
        //}
        //void OnMouseDrag()
        //{
        //    MemberCall("OnMouseDrag");
        //}
        //void OnMouseEnter()
        //{
        //    MemberCall("OnMouseEnter");
        //}
        //void OnMouseExit()
        //{
        //    MemberCall("OnMouseExit");
        //}
        //void OnMouseOver()
        //{
        //    MemberCall("OnMouseOver");
        //}
        //void OnMouseUp()
        //{
        //    MemberCall("OnMouseUp");
        //}
        //void OnMouseUpAsButton()
        //{
        //    MemberCall("OnMouseUpAsButton");
        //}

        //void OnTriggerEnter(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerEnter", param);
        //}
        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerEnter2D", param);
        //}
        //void OnTriggerExit(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerExit", param);
        //}
        //void OnTriggerExit2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerExit2D", param);
        //}
        //void OnTriggerStay(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerStay", param);
        //}
        //void OnTriggerStay2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerStay2D", param);
        //}

        //void OnTriggerEnter(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerEnter", param);
        //}
        //void OnTriggerEnter2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerEnter2D", param);
        //}
        //void OnTriggerExit(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerExit", param);
        //}
        //void OnTriggerExit2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerExit2D", param);
        //}
        //void OnTriggerStay(Collider other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider);
        //    param[0].value = other;
        //    MemberCall("OnTriggerStay", param);
        //}
        //void OnTriggerStay2D(Collider2D other)
        //{
        //    List<CSLE.CLS_Content.Value> param = new List<CSLE.CLS_Content.Value>();
        //    param.Add(new CSLE.CLS_Content.Value());
        //    param[0].type = typeof(Collider2D);
        //    param[0].value = other;
        //    MemberCall("OnTriggerStay2D", param);
        //}


    }
}