HangmanGame test = new HangmanGame();
test.StartGame();

class HangmanGame {
  private Library WordsDB {get; set;}
  private Player Player1 {get; set;}
  private string SecretWord {get; set;}
  private char[] WordGuess {get; set;}
  private char[] WrongGuesses {get; set;}

  public HangmanGame() {
    WordsDB = new Library();
    Player1 = new Player();
    SecretWord = WordsDB.RandomWord;
    WordGuess = new string('-', SecretWord.Length).ToCharArray();
    WrongGuesses = new char[5];
  }
  
  public void StartGame() {
    int RemainingMistakes = 5;
    int secretIndex = 0;
    while (RemainingMistakes > 0 && !HasWon()) {
      Console.Clear();
      Console.WriteLine($"Words {string.Join("", WordGuess)} | Remaining: {RemainingMistakes} | Incorrect {WrongLetters()} | Guess: {Player1.PlayerPick}");
      Player1.Pick();
      if (SecretWord.Contains(Player1.PlayerPick)) {
        for (int i = 0; i < SecretWord.Length; i++) {
          if (SecretWord[i] == Player1.PlayerPick)
          WordGuess[i] = Player1.PlayerPick;
        }
      }
      else {
        RemainingMistakes--;
        WrongGuesses[secretIndex] = Player1.PlayerPick;
        secretIndex++;
      }
    }
    if (HasWon()) {
    Console.WriteLine($"Words {string.Join("", WordGuess)} | Remaining: {RemainingMistakes} | Incorrect: {WrongLetters()} | Guess: {Player1.PlayerPick}");
    Console.WriteLine("You Won!");
    }
    else {
      Console.WriteLine("GAME OVER");
    }
  }

  public string WrongLetters() {
    return string.Join("", WrongGuesses);
  }

  public bool HasWon() {
    if (string.Join("", WordGuess).Equals(SecretWord))
      return true;
    else
      return false;
  }
}

class Player {
  public char PlayerPick {get; private set;} 
  public List<char> PlayerHistory {get; private set;}

  public Player() {
    PlayerPick = ' ';
    PlayerHistory = new List<char>();
  }

  public void Pick() {
    do {
    ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true means don't show input in console. 
      PlayerPick = Char.ToLower(keyInfo.KeyChar);
    } while (PlayerHistory.Contains(PlayerPick));
    PlayerHistory.Add(PlayerPick);
  }
}

class Library {
  static readonly string[] AllWords = new string[] {
  "apple", "book", "car", "dog", "egg", "fish", "game", "hat", "ice", "juice",
  "key", "lamp", "moon", "nose", "orange", "pen", "queen", "rain", "sun", "tree",
  "umbrella", "van", "water", "xray", "yarn", "zebra", "ball", "cat", "desk", "ear",
  "frog", "goat", "hill", "ink", "jar", "kite", "leaf", "milk", "net", "owl",
  "pig", "quiz", "rope", "star", "train", "vase", "wind", "yak", "zip", "chair"
  };
  public string RandomWord {get; private set;}

  public Library() {
    RandomWord = "";
    GenerateWord();
  }

  public void GenerateWord() {
    Random rng = new Random();
    RandomWord = AllWords[rng.Next(AllWords.Length)];
  }
}
