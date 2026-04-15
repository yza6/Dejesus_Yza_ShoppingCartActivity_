# Day 1

I developed a Product class to serve as a base for the inventory, allowing for the organized storage of item names, pricing, and stock levels. 
Additionally, I initialized a data structure to manage the store's products and successfully configured the project’s version control by linking it to a 
GitHub repository.

# Day 2

On Day 2, I focused on making the program interactive so that a user can actually shop for items. I created a CartItem class to act as a container for whatever the user picks, keeping track of the product, the quantity, and the total cost for that specific item. This was a major step because it allowed the program to "remember" what was placed in the basket rather than just showing a list of products. I also added a shopping loop that keeps the store menu visible and asks the user to enter a product number. To make the program more professional and avoid crashes, I used "safety checks" to ensure that if a user types a letter instead of a number, the program handles it gracefully. I also included logic to check if there is enough stock available before adding an item to the cart, ensuring the inventory stays accurate as the user continues to shop.
