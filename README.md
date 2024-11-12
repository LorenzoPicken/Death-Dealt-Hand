<h1>Death Dealt Hand</h1>
<p>is a short playing card game that introduces a twist to the traditional italian card game of Scopa. This is done by infusing horror elements into an otherwise calming experience through fine tunned lighting and post processing effects as well as carefully selected sound effects and ambiance. Inspired by games such as Buckshot Roulette and Inscription, we aimed to create a chilling but memorable game that brings a unique vibe to the playing card table. This game was a collaborative work made in unity by a team of three programmers as a final project.</p>
<br><br>

<h2>Gameplay</h2>
<p>Death Dealt Hand's gameplay is simple and mainely revolves around the same mechanics found in a traditional game of Scopa, aside from a couple of small differences. Both the player and the enemy AI will take turns playing a card from their hand until the entire deck has been emptied. From here, points are counted and a new round begins. This will last until either player reaches 11 points, in which case the game will end.</p>
<br>

<h3>General Rules</h3>
<p>Just like in Scopa, a match begins by distributing three cards to each player and placing four cards down on the table. From here the starting player must try and pick up a card or sum of cards from the table with an identical value to a chosen card from their hand. If this is not possible, then they must discard a card, placing it down on the table. Once the current player has played a card from their hand, their turn ends, and the next opponent begins theirs. Collected cards go into the respective player's collection deck which will be counted at the end of a round during point calculation. Once both players have played all their cards, which takes 6 turns/ three per player, a new batch of 3 cards is dealt and the process restarts. This will last until the deck from which new cards are drawn from is empty. Once this happens, all remaining table cards will go to the last player to have picked up and then point calculation begins. Three possible points can be scored each round and are determined as follows:</p>
<ul>
  <li><p>1 point to the player with the most collected cards.</p></li>
  <li><p>1 point to the player who managed to collect the most cards belonging to the suit of SUNS.</p></li>
  <li><p>1 point to the player with the SEVEN of SUNS.</p></li>
</ul>
<br>

<p>To achieve this gameplay loop, three main systems work in unison to deliver the experience. They are </p>
<ul>
  <li><a href="https://github.com/LorenzoPicken/Death-Dealt-Hand/blob/main/README.md#the-table">The Table</a></li>
  <li><a href="https://github.com/LorenzoPicken/Death-Dealt-Hand/blob/main/README.md#the-opponent">The Opponent</a></li>
  <li><a href="https://github.com/LorenzoPicken/Death-Dealt-Hand/blob/main/README.md#the-effect-deck">The Effect Deck</a></li>
</ul>
<br><br>

<h3>The Table</h3>
<p>The Table is the name that we gave to the dealer or more accurately, the game manager. Responsible for overseeing the game state and tracking important values, the table provides crutial information to various other systems which allows them to perform important procedures. Information such as score, player turns, deck size, available table slots, etc, are all accounted for by this manager. Additionally, it is also responsible for executing tasks such as distributing cards, shuffling the deck, calculating points and managing the game state. Systems such as the player controller and enemy ai rely heavily on variables that the table tracks to function properly.</p>
<br>

<h3>The Opponent</h3>
<p>The opponent is the algorythm responsible for the decision making of the enemy player and is by far the most complexe system in the game. In order to achieve a realistic and challenging opponent for the player, the algorythm will run simulated senarious of all possible plays internally then make the appropriate decision based on a point system. The amount of points each individual play yields is calculated by subtracting the risk of the play from the reward. In order to find the reward of a given outcome, the simulated play is compared to a list of template outcomes with pre-set reward values. After finding the closest template to the current situation, the reward level for that outcome will be set to the default reward value of the compared template. Following this, the algorithm will calculate the risk factor. The risk can be as low as 0 or as high as 3 and directly correlates to the likelyhood that after the simulated play is executed, the player will be left in a favorable position to score points. From here the play with the best score is selected and depending on the final reward value, the algorythm will decide if the outcome is worth executing or if giving up a card to avoid potentially helping the player is more ideal in it's current position. Besides the point system, the current score also affects the opponents decision making. If the player is winning, the algorythm will play more agressively and make riskier choices in an attempt to catch up. Inversely, if it has the upper hand over the player, it will play more defensively, avoiding risky manuvers.</p>
<br>

<h3>The Effect Deck</h3>
<p>Finally, The Effect Deck is the final piece of the puzzle that brings the whole experience together. In an attempt to diversify the experience and introduce a more enjoyable gameplay loop, we came up with the idea of an additional card deck that could be blended seamlessly into a traditional game of Scopa. What we came up with was The Effect Deck, a special collection of cards that could offer an advantage to the player who draws them.</p>
<p>Within the current version of the game, there are FIVE unique cards that can be drawn from the supplementary deck. These are:</p>

<ul>
  <li>
    <h4>The Takers</h4>
    <p>Steal three random cards from your opponents collected cards</p>
    <p>*If the opponent has three or less cards in their collection deck, steal the entire deck. Effect is negated if the opponent has no cards.</p>
  </li>

  <li>
    <h4>Wheel Of Fortune</h4>
    <p>Shuffle all cards from your hand into the deck and draw the same number of new cards from the top of the pack</p>
    <p>*This card's effect is negated if the deck is empty.</p>
  </li>

  <li>
    <h4>The Butcher</h4>
    <p>If your opponent's score is above zero, subtract their current score by one</p>
    <p>*For balancing, this card has the lowest chance of being drawn.</p>
  </li>

  <li>
    <h4>Blood Pact</h4>
    <p>At the end of the current match, collect all remaining cards from the table.</p>
    <p>*This effect is negated if the opponent draws this effect after you.</p>
  </li>

  <li>
    <h4>Evil Eye</h4>
    <p>Reveals the opponent's hand to the player</p>
    <p>*This is the only effect that cannot be drawn by the enemy AI</p>
  </li>
</ul>
<br>

<p>To draw from the effect deck, a token must be used in exchange. These tokens are awarded to both the player and the enemy throughout the match based on two conditions. First, a player recieves one token every time they clear the table of cards. The second condition directly corelated to the performance of a player at the end of a round. Winning the round will award 2 tokens to the losing player and none to the victor. A tie on the other hand will award a token to each player. A maximum limit of three tokens is enstated at all times throughout the match and an effect card must be drawn before playing a regular card and is limited to one draw per turn.</p>
