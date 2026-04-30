# AI Usage

Instead of just having AI write code for me, I used it as a technical resource to help me understand the "why" behind C#. It acted as a tutor that helped me improve my logic. Whenever I got stuck, I asked for examples so I could learn how to build the solutions myself.

One of the biggest things I worked on was the "Continue Shopping" and "Menu Choice" prompts. Originally, if a user pressed any key that wasn't 'N', the program just assumed they meant 'Yes'. I worked with the AI to set up a while loop and a validChoice flag so the program only moves forward when it gets a real 'Y' or 'N'. This made the app way more reliable.

I built a Product class to keep track of names, prices, and stock, and a CartItem class for the shopping cart. The AI helped me brainstorm how to make the store menu stay visible so a user can keep browsing without the app resetting.

I added "safety checks" to handle mistakes, like using int.TryParse so the app doesn't crash if someone types a letter instead of a number.

I used AI to double-check that my C# code actually matched my project flowchart. It helped me make sure my logic was sound from start to finish.

Since I was new to GitHub, I used AI to learn the workflow of creating a repository and linking my local project online so my progress was safely backed up.

Questions I Asked:

"How do I make a loop that only accepts 'Y' or 'N' and tells the user if they typed something wrong?"
"How do I use int.TryParse to validate what a user types?"
"How do I format a price so it always shows two decimal places?"
"How can I remove an item from an array without leaving a gap in the middle?"
"Can you help me fix the errors in my AddStock method?"
