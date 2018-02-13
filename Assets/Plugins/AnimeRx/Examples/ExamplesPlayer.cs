using System.Linq;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AnimeRx.Development
{
    public class ExamplesPlayer : MonoBehaviour
    {
        [SerializeField] private Dropdown dropdown;
        [SerializeField] private Button playButton;
        [SerializeField] private Examples examples;

        public void Start()
        {
            dropdown.options = Examples.Samples.Select(x => new Dropdown.OptionData(x)).ToList();
            playButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var select = dropdown.options[dropdown.value].text.Split(':')[0];
                    examples.Initialize();
                    examples.gameObject.SendMessage("Sample" + select);
                })
                .AddTo(this);
        }
    }
}
