<h1>Death Dealt Hand</h1>
<p>is a short playing card game that introduces a twist to the traditional italian card game of Scopa. This is done by infusing horror elements into an otherwise calming experience through fine tuned lighting and post processing effects as well as carefully selected sound effects and ambiance. Inspired by games such as Buckshot Roulette and Inscription, we aimed to create a chilling but memorable game that brings a unique vibe to the playing card table. This game was a collaborative work made in unity by a team of three programmers as a final project.</p>
<br><br>

<h2>Gameplay</h2>
<p>Death Dealt Hand's gameplay is simple and mainly revolves around the same mechanics found in a traditional game of Scopa, aside from a couple of small differences. Both the player and the enemy AI will take turns playing a card from their hand until the entire deck has been emptied. From here, points are counted and a new round begins. This will last until either player reaches 11 points, in which case the game will end.</p>
<a href="https://youtu.be/A6HeU3YFyyA" target="_blank">Gameplay Demo Video</a>
<br><br>

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
<p>The Table is the name that we gave to the dealer or more accurately, the game manager. Responsible for overseeing the game state and tracking important values, the table provides crucial information to various other systems which allows them to perform important procedures. Information such as score, player turns, deck size, available table slots, etc, are all accounted for by this manager. Additionally, it is also responsible for executing tasks such as distributing cards, shuffling the deck, calculating points and managing the game state. Systems such as the player controller and enemy ai rely heavily on variables that the table tracks to function properly.</p>
<br>

<h3>The Opponent</h3>
<p>The opponent is the algorithm responsible for the decision making of the enemy player and is by far the most complex system in the game. In order to achieve a realistic and challenging opponent for the player, the algorithm will run simulated scenarios of all possible plays internally then make the appropriate decision based on a point system. The amount of points each individual play yields is calculated by subtracting the risk of the play from the reward. In order to find the reward of a given outcome, the simulated play is compared to a list of template outcomes with pre-set reward values. After finding the closest template to the current situation, the reward level for that outcome will be set to the default reward value of the compared template. Following this, the algorithm will calculate the risk factor. The risk can be as low as 0 or as high as 3 and directly correlates to the likelihood that after the simulated play is executed, the player will be left in a favorable position to score points. From here the play with the best score is selected and depending on the final reward value, the algorithm will decide if the outcome is worth executing or if giving up a card to avoid potentially helping the player is more ideal in its current position. Besides the point system, the current score also affects the opponents decision making. If the player is winning, the algorithm will play more aggressively and make riskier choices in an attempt to catch up. Inversely, if it has the upper hand over the player, it will play more defensively, avoiding risky manoeuvres.</p>
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

<p>To draw from the effect deck, a token must be used in exchange. These tokens are awarded to both the player and the enemy throughout the match based on two conditions. First, a player receives one token every time they clear the table of cards. The second condition directly correlated to the performance of a player at the end of a round. Winning the round will award 2 tokens to the losing player and none to the victor. A tie on the other hand will award a token to each player. A maximum limit of three tokens is instituted at all times throughout the match and an effect card must be drawn before playing a regular card and is limited to one draw per turn.</p>
<br><br>

<h2>Card Designs</h2>
<p>While the designs of the main deck of scopa cards are ones found on traditional italian playing cards, the designs for the effect cards were a mix of fair use images found online and original designs stencilled or drawn in Microsoft Paint 3D and overlaid as materials on custom 3D card models made in Blender. The same process was used to model every card used in the game as well as the deck asset.</p>
<br><br>

<h2>Audio System and Design</h2>
<p>Audio always plays a big roll in horror by building suspense and providing ambiance and this is no different for our game. Taking inspiration from some of our favorite games and movies, we attempted to create an audio system that provided just the right amount of scare factor without being overbearing or loosing its effect too early in the game. We did this with a selection of free online audio as well as self recorded audio which we edited and spliced in a music production software. This audio is then used in one of two ways. For ambiance or as part of scares! Ambiance is mainly comprised of the weather audio which escalates over the course of the game. In the beginning light wind can be heard outside, rustling trees and shaking the windows lightly, however, the closer the opponent gets to winning, the more violent the weather becomes eventually turning into a torrential thunderstorm that rumbles in the background and provides some tension to the game. For scares, an array of audio from footsteps to slamming doors to whispers play at random intervals throughout the game, just far enough apart to catch a distracted person off guard.</p>
<br><br>

<h2>Cut Content</h2>
<p>Due to a couple of feature delays and a tight development period, some planned content never made it into the final version of the game. Here is a list of all of it, in no particular order.</p>
<br>

<h3>Introduction and Game Over Sequence</h3>
<p>Originally, we had planned to have short animated sequences using cinemachine cameras and model animations to infuse the game with a bit more life and create a small sense of connection between the user and the player character. This was supposed to be accomplished with a small interactive sequence at the beginning of the game which would have the player open the pack of cards, triggering a power outage and initialising the start of the game, as well as read a tutorial on how to play the game from the back of the card box. Other sequences were also planned for when the player won or lost the game which would each trigger unique scripted jumpscares.</p>
<br>

<h3>Visual Scare Events</h3>
<p>While there are audio based scares in the game, there were also planned to be visual events that would be used with the audio to invoke an even stronger sense of danger in the player. Things such as shadowy figures walking around in the background, hands reaching out from behind the player's head or even the opponent's character model moving during flashes of lightning were all planned but never implemented due to a lack of time.</p>
<br>

<h3>Enemy Aggressiveness</h3>
<p>This is a feature that can be found within the game but that was never implemented to match the complete vision we had. While the weather does worsen as the enemy AI approaches victory and its play style does change to reflect the score, there were also plans to add challenge and additional effects to better display progress within the game. Firstly, once the player scored three points, a timer system was supposed to be introduced giving the player a smaller window for decision making. This timer would shorten from 8 seconds upon its introduction to 5 once the player reaches 7 points. Initially, the penalty for not playing within the given time would be a forced discard of a random card from the player's hand, however towards the later part of the game, failure to play within the time would result in a jumpscare and an immediate game over. In addition to this, as the enemy gained points they would also slowly become more visible and take on a more sinister form, showing that the player was at the risk of losing the game.</p>
<br>

<h3>Improved Juice</h3>
<p>More impactful animations and visual effects were planned in order to better display events taking place within the game but were never completed in the end.</p>
<br><br><br>

<h2>AI Development and Testing</h2>
<p>The AI (algorithm) is responsible for enemy decision making and went through many iterations and tests before we finally landed on the final version. Originally, this algorithm was developed separately from the rest of the game, in a Visual Studio console application in order to get a more straightforward and decoupled environment. This version required a user to manually enter the hand and table into two separate lists then run the application. Eventually, once completely bug free, the algorithm was tested in actual games of scopa against members of the team as well as some experienced scopa players and managed to win about as many games as any regular person would. Finally, in order to accommodate less experienced players, a few parameters were tweaked to make the system a bit less difficult to play against and it was then implemented into the Unity project. Just like a real opponent, the algorithm does not cheat or gain any additional insight on the player's hand. It makes its decision only through information it knows like the visible cards on the table and in its hand, essentially making it so that the winner of each round is determined by the luck of the cards like in most other similar tabletop games.</p>
