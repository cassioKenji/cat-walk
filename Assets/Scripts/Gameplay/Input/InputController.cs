using System;
using Gameplay.Decks;
using Gameplay.NewInput;
using Gameplay.NewInput.Decks;
using Rewired;
using UnityEngine;

namespace Gameplay.Input
{
    public class InputController : MonoBehaviour
    {
        public DeckManager deckManager;
        public InputBroadcaster inputBroadCaster;

        public Boneco.Boneco boneco;
        
        public int playerId = 0;
        public Rewired.Player player;
        
        private const string _xAxisActionName = "MoveHorizontal";
        private const string _yAxisActionName = "MoveVertical";
        private const string _jumpActionName  = "Jump";
        private const string _meleeActionName = "Melee";

        [SerializeField]
        private Vector2 _inputVector;
        
        public Vector2 InputVector { get; }

        [SerializeField] 
        private float xInputValue;

        [SerializeField]
        private bool _jumpoButtonDown = false;
        public bool JumpButtonDown { get; set; }

        [SerializeField]
        private bool _jumpButtonUp = false;
        public bool JumpButtonUp { get; set; }

        [SerializeField]
        private bool _meleeButtonDown = false;

        //private InputFilter _inputFilter;
        public bool MeleeButtonDown { get; set; }

        private void Awake()
        {
            deckManager = GetComponent<DeckManager>();
            boneco = GetComponent<Boneco.Boneco>();
        }

        private void Start()
        {
            inputBroadCaster = deckManager.GetNewInputBroadcaster();
            player = ReInput.players.GetPlayer(playerId);
            
            //TODO registrar esse evento para testar
        }
        
        private void Update()
        {
            GetInput();
            UpdateInputBroadcaster();
        }

        void GetInput()
        {
            _inputVector    = player.GetAxis2DRaw(_xAxisActionName, _yAxisActionName);
            xInputValue = player.GetAxis(_xAxisActionName);
            //Mathf.Clamp(xInputDirecion, -0.001f, 0.001f);
            
            JumpButtonDown  = player.GetButtonDown(_jumpActionName);
            JumpButtonUp    = player.GetButtonUp(_jumpActionName);
            MeleeButtonDown = player.GetButtonDown(_meleeActionName);
            
            ProcessInput();
        }

        void ProcessInput()
        {
            if (JumpButtonDown)
            {
                boneco.OnJumpButtontDown();
            }
            
            if (MeleeButtonDown)
            {
                boneco.OnMeleeButtonDown();
            }
        }

        void UpdateInputBroadcaster()
        {
             inputBroadCaster.UpdateInputs(_inputVector, xInputValue);
        }
    }
}
