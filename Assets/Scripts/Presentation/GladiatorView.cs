using DG.Tweening;
using Presentation.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Presentation
{
    public class GladiatorView : MonoBehaviour
    {
        [SerializeField] private string gladiatorId;
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private Animator animator;
        [SerializeField] private Transform spriteTransform;

        public string Id => gladiatorId;

        public void Init(GladiatorData data)
        {
            gladiatorId = data.GetId();
            nameLabel.text = data.Name;
        }

        public void FaceCenter(Vector3 center)
        {
            if (center.x < spriteTransform.position.x)
                spriteTransform.localScale = new Vector3(-Mathf.Abs(spriteTransform.localScale.x), spriteTransform.localScale.y, spriteTransform.localScale.z);
            else
                spriteTransform.localScale = new Vector3(Mathf.Abs(spriteTransform.localScale.x), spriteTransform.localScale.y, spriteTransform.localScale.z);
        }

        public Tween JumpToCenter(Vector2 center)
        {
            animator.SetTrigger("Jump");
            RectTransform rt = (RectTransform)transform;

            Vector2 start = rt.anchoredPosition;
            float height = 150f;
            float duration = 0.5f;

            var seq = DOTween.Sequence();

            // Подготовка
            seq.Append(rt.DOScale(0.85f, 0.12f));

            // Полёт по дуге
            seq.Append(DOTween.To(
                () => 0f,
                t =>
                {
                    Vector2 pos = Vector2.Lerp(start, center, t);
                    float y = Mathf.Sin(t * Mathf.PI) * height;
                    rt.anchoredPosition = pos + Vector2.up * y;
                },
                1f,
                duration
            ));

            // Приземление
            seq.Append(rt.DOScale(1.1f, 0.08f));
            seq.Append(rt.DOScale(1f, 0.1f));

            return seq;
        }

        public Tween ThrowOut(Vector3 target)
        {
            animator.SetTrigger("Lose");
            return transform
                .DOMove(target, 0.6f)
                .SetEase(Ease.InBack);
        }

        public void PlayVictory()
        {
            animator.SetTrigger("Win");
            transform.DOPunchScale(Vector3.one * 0.2f, 2f);
        }
    }

}
