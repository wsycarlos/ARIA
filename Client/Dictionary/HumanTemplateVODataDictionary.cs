// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Game.Template
{
    using UnityEngine;
    
    
    public class HumanTemplateVODataDictionary : ITemplateDictionary
    {
        
        private System.Collections.Generic.List<HumanTemplateVO> _itemlist;
        
        private System.Collections.Generic.Dictionary<int, HumanTemplateVO> _itemdic;
        
        public virtual System.Collections.Generic.List<HumanTemplateVO> ItemList
        {
            get
            {
                return _itemlist;
            }
            set
            {
                _itemlist = value;
            }
        }
        
        public virtual System.Collections.Generic.Dictionary<int, HumanTemplateVO> ItemDic
        {
            get
            {
                return _itemdic;
            }
            set
            {
                _itemdic = value;
            }
        }
        
        public void Init(System.Collections.Generic.List<object> list)
        {
            _itemlist = new System.Collections.Generic.List<HumanTemplateVO>();
            int length = list.Count;
            for (int i = 0; (i < length); i = (i + 1))
            {
                _itemlist.Add(list[i] as HumanTemplateVO);
            }
            this.InitDictionary();
        }
        
        private void InitDictionary()
        {
            int length = _itemlist.Count;
            _itemdic = new System.Collections.Generic.Dictionary<int, HumanTemplateVO>();
            for (int i = 0; (i < length); i = (i + 1))
            {
                try
                {
                    _itemdic.Add(_itemlist[i].id, _itemlist[i]);
                }
                catch (System.Exception ex)
                {
                    Debug.LogWarning(ex.Message);
                }
            }
        }
    }
}
