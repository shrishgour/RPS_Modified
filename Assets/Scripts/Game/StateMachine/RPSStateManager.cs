using Core.Events;
using Core.Services;
using Core.StateMachine;
using Game.Character;
using Game.Events;

namespace Game.StateMachine
{
    public class RPSStateManager : StateMachine<RPSStateManager>
    {
        private Player player;
        private Bot bot;
        private GameResultEvent gameResultData;
        private int score;
        public Player Player => player;
        public Bot Bot => bot;
        public GameResultEvent GameResultData => gameResultData;
        public int Score => score;

        private void Start()
        {
            EventManager.Instance.AddListener<GameResultEvent>(OnGameResult);
            InitPlayers();
            BeginGame();
        }

        private void OnGameResult(GameResultEvent e)
        {
            gameResultData = e;
        }

        public void BeginGame()
        {
            score = 0;
            ChangeState(new BeginState(this));
        }

        public void InitPlayers()
        {
            player = null;
            bot = null;

            player = new Player();
            bot = new Bot();
        }

        public void UpdateScore()
        {
            score++;
            var highScore = ServiceRegistry.Get<UserState>().HighScoreValue;
            if (score > highScore)
            {
                ServiceRegistry.Get<UserState>().HighScoreValue = score;
            }
        }
    }
}