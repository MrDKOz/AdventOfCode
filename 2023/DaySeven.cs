using AdventOfCode.Helpers;

namespace AdventOfCode._2023;

public class DaySeven
{
    private CamelCards? _camelCards;
    
    [SetUp]
    public void Setup()
    {
        _camelCards = new CamelCards(PuzzleInput.Load(2023, 7));
    }

    [Test]
    public void PartOne()
    {
        Console.WriteLine($"Day Seven, Part One Answer: {_camelCards?.PartOne()}");
    }

    [Test]
    public void PartTwo()
    {
        Console.WriteLine($"Day Seven, Part Two Answer:");
    }
}

public class CamelCards
{
    public List<CardsInHand> Hands = new();

    public CamelCards(IEnumerable<string> input)
    {
        ProcessInput();
        Hands = Hands.OrderByDescending(hand => hand).ToList();
        return;

        void ProcessInput()
        {
            foreach (var split in input.Select(line => line.Split(' ')))
            {
                AddHand(split.First(), split.Last());
            }
        }
    }

    public int PartOne()
    {
        var runningTotal = 0;
        
        foreach (var hand in Hands)
        {
            var handValue = hand.Bid * (Hands.IndexOf(hand) + 1);
            runningTotal += handValue;
        }

        return runningTotal;
    }

    private void AddHand(string cards, string bid) => Hands.Add(new CardsInHand(cards, Convert.ToInt32(bid)));

    public class CardsInHand : IComparable<CardsInHand>
    {
        private readonly Card[] _cards = new Card[5];
        public readonly int Bid;
        private Hand _hand = Hand.None;
        private record Card(char Name, int Strength);

        public CardsInHand(string cards, int bid)
        {
            Bid = bid;

            CheckForHandType(cards);
            ProcessCards(cards);
        }

        private void ProcessCards(string cards)
        {
            var currentIndex = 0;

            foreach (var card in cards)
            {
                var strength = card switch
                {
                    'A' => 13,
                    'K' => 12,
                    'Q' => 11,
                    'J' => 10,
                    'T' => 9,
                    '9' => 8,
                    '8' => 7,
                    '7' => 6,
                    '6' => 5,
                    '5' => 4,
                    '4' => 3,
                    '3' => 2,
                    '2' => 1,
                    _ => throw new Exception($"Invalid card '{card}'")
                };
                
                _cards[currentIndex] = new Card(card, strength);
                currentIndex++;
            }
        }
        
        private void CheckForHandType(string cardsAsString)
        {
            var characterCounts = GetCharacterCounts(cardsAsString);

            foreach (var characterCount in characterCounts)
            {
                switch (characterCount.Value)
                {
                    case 5:
                        // All cards match
                        _hand = Hand.FiveOfAKind;
                        break;
                    case 4:
                        // All but one card match
                        _hand = Hand.FourOfAKind;
                        break;
                    case 3:
                        // Three cards match, if there is a pair it's a full house
                        _hand = characterCounts.Count(cc => cc.Value == 2) == 1
                            ? Hand.FullHouse
                            : Hand.ThreeOfAKind;
                        break;
                    case 2:
                        // Two cards match, if there is another pair it's two pair
                        var moreThanOnePair = characterCounts.Count(cc => cc.Value == 2) > 1;
                        _hand = moreThanOnePair ? Hand.TwoPair : Hand.OnePair;
                        break;
                    default:
                        // All cards are different
                        _hand = Hand.HighCard;
                        break;
                }

                if (_hand == Hand.None) continue;
                break;
            }
        }

        private static Dictionary<char, int> GetCharacterCounts(ReadOnlySpan<char> span)
        {
            var charCounts = new Dictionary<char, int>();

            foreach (var c in span)
            {
                if (charCounts.TryGetValue(c, out var value))
                {
                    charCounts[c] = ++value;
                }
                else
                {
                    charCounts[c] = 1;
                }
            }

            return charCounts.OrderByDescending(entry => entry.Value)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public int CompareTo(CardsInHand? other)
        {
            if (other is null) return 1;
            if (_hand > other._hand) return 1;
            if (_hand < other._hand) return -1;

            var index = 0;
            while (_cards[index] == other._cards[index] && index < _cards.Length - 1)
            {
                index++;
            }
            
            return other._cards[index].Strength.CompareTo(_cards[index].Strength);
        }
    }

    private enum Hand
    {
        FiveOfAKind,
        FourOfAKind,
        FullHouse,
        ThreeOfAKind,
        TwoPair,
        OnePair,
        HighCard,
        None
    }
}