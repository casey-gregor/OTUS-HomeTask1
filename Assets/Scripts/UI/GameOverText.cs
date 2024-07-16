using TMPro;

namespace ShootEmUp
{
    public sealed class GameOverText : IGameFinishListener
    {
        private TextMeshProUGUI textMeshPro;


        public GameOverText(TextMeshProUGUI textMeshPro)
        {
            this.textMeshPro = textMeshPro;
            this.textMeshPro.gameObject.SetActive(false);
        }

        public void OnFinish()
        {
            this.textMeshPro.gameObject.SetActive(true);
            this.textMeshPro.text = "Game Over";
            this.textMeshPro.fontSize = 80;
        }

    }

}
