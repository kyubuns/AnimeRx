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
            playButton.OnClickAsObservable()
                .Subscribe(_ =>
                {
                    var select = dropdown.options[dropdown.value].text;
                    examples.Initialize();
                    examples.gameObject.SendMessage(select);
                })
                .AddTo(this);
        }
    }
}
