using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Data
{
    public class LangResolver : MonoBehaviour
    {
        private const char Separator = '=';
        private readonly Dictionary<string, string> _lang = new Dictionary<string, string>();
        private string _language;
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            ReadProperties();
            //Debug.Log(_lang.Count);
            //Debug.Log(_lang.Keys.First());
            //Debug.Log(_lang.Values.First());
        }
        public void ReadProperties()
        {
            
            _language = GameManager.instance.getLanguage();
            var file = Resources.Load<TextAsset>(_language.ToString());
            if (file == null)
            {
                file = Resources.Load<TextAsset>(SystemLanguage.English.ToString());
                _language = SystemLanguage.English.ToString();
            }
            foreach (var line in file.text.Split('\n'))
            {
                var prop = line.Split(Separator);
                _lang[prop[0]] = prop[1];
            }
        }

        public void ResolveTexts()
        {
            var allTexts = Resources.FindObjectsOfTypeAll<LangText>();
            foreach (var langText in allTexts)
            {
                var text = langText.GetComponent<Text>();
                text.text = Regex.Unescape(_lang[langText.Identifier]);
            }

            //Marquees
            var allShopControls = Resources.FindObjectsOfTypeAll<ShopSelector>();
            foreach (var shopSelector in allShopControls)
            {
                if(!string.IsNullOrEmpty(shopSelector.getSelectedItem().description))
                    shopSelector.setTextMarquee(resolveText(shopSelector.getSelectedItem().description));
            }

            //TabSelector
            var allTabControls = Resources.FindObjectsOfTypeAll<TabDressRoom>();
            foreach (var tabSelector in allTabControls)
            {
                tabSelector.updateVisuals();
            }

            //ButtonSelector
            var dressRooms = Resources.FindObjectsOfTypeAll<DressRoomMenu>();
            foreach (var dressRoom in dressRooms)
            {
                dressRoom.updateButtonSelect();
            }

            //sHOP
            var shops = Resources.FindObjectsOfTypeAll<ShopMenu>();
            foreach (var shop in shops)
            {
                shop.updateButtonShop();
            }
        }

        public string resolveText(string key)
        {
           
            return Regex.Unescape(_lang[key]); ;
        }
    }
}
