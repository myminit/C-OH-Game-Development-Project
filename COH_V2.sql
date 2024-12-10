create schema cohcode;
use cohcode;

CREATE TABLE user (
    user_id INT PRIMARY KEY AUTO_INCREMENT,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(255) NOT NULL,  
    email VARCHAR(100) NOT NULL
);

CREATE TABLE levels (
    level_id INT PRIMARY KEY AUTO_INCREMENT,
    level_name VARCHAR(50) NOT NULL,
    description TEXT
);

CREATE TABLE play (
    user_id INT NOT NULL,
    level_id INT NOT NULL,
    complete BOOLEAN NOT NULL DEFAULT false,
    PRIMARY KEY (user_id, level_id),
    FOREIGN KEY (user_id) REFERENCES user(user_id),
    FOREIGN KEY (level_id) REFERENCES levels(level_id)
);

CREATE TABLE enemy (
    enemy_id INT PRIMARY KEY AUTO_INCREMENT,
    enemy_name VARCHAR(50) NOT NULL,
    type VARCHAR(30) NOT NULL,
    enemy_description TEXT
);

CREATE TABLE quest (
    quest_id INT AUTO_INCREMENT,
    level_id INT NOT NULL,
    quest_q TEXT NOT NULL,
    quest_a TEXT NOT NULL,
    spoil TEXT,
	hint TEXT,
    PRIMARY KEY (quest_id, level_id),
    FOREIGN KEY (level_id) REFERENCES levels(level_id)
);

CREATE TABLE rank_ (
    rank_id INT AUTO_INCREMENT,
    user_id INT NOT NULL,
    name VARCHAR(50) NOT NULL,
    last_level VARCHAR(50) NOT NULL,
    total_time DOUBLE NOT NULL,
    PRIMARY KEY (rank_id, user_id),
    FOREIGN KEY (user_id) REFERENCES user(user_id)
);

CREATE TABLE stats (
    stat_id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
    rank_id INT NOT NULL,
    time DOUBLE NOT NULL,
    FOREIGN KEY (rank_id) REFERENCES rank_(rank_id)
);

CREATE TABLE correct (
    quest_id INT NOT NULL,
    user_id INT NOT NULL,
    stat_id INT NOT NULL,
    is_correct BOOLEAN NOT NULL DEFAULT false,
    PRIMARY KEY (quest_id, user_id, stat_id),
    FOREIGN KEY (quest_id) REFERENCES quest(quest_id),
    FOREIGN KEY (user_id) REFERENCES user(user_id),
    FOREIGN KEY (stat_id) REFERENCES stats(stat_id)
);

CREATE TABLE includes (
    level_id INT NOT NULL,
    enemy_id INT NOT NULL,
    PRIMARY KEY (level_id, enemy_id),
    FOREIGN KEY (level_id) REFERENCES levels(level_id),
    FOREIGN KEY (enemy_id) REFERENCES enemy(enemy_id)
);

INSERT INTO levels (level_id, level_name, description) 
VALUES 
(1, 'Level 1', 'Learn about basic data types in C and how to declare variables, including defining basic types like int, float, and char for use in simple programs that are appropriate for the data being handled.'),
(2, 'Level 2', 'Understand how to receive and display data in C using the `scanf` and `printf` functions, including how to format output for different types of data such as numbers, strings, and other variables.'),
(3, 'Level 3', 'Learn how to use variables and constants in expressions for mathematical calculations, utilizing operators such as +, -, *, /, and % to perform calculations in a program.'),
(4, 'Level 4', 'Learn how to use conditional statements such as `if`, `else if`, and `else` to create branching logic in a program, allowing decisions to be made based on specified conditions.'),
(5, 'Level 5', 'Understand how to use loops such as `for`, `while`, and `do-while` to repeat a set of instructions multiple times, allowing more flexibility in the program execution based on given conditions.'),
(6, 'Level 6', 'Learn how to declare and use functions to break down complex code into smaller, reusable parts, making programs more modular by taking parameters and returning values from functions.'),
(7, 'Level 7', 'Understand and use arrays to store multiple values in a single variable, accessing specific elements based on their index, making it easier to manage large amounts of data in a program.'),
(8, 'Level 8', 'Learn how to combine functions and arrays to create programs that can handle complex data operations, such as calculating averages from an array of data.'),
(9, 'Level 9', 'Study error handling techniques to manage potential problems in your program, such as checking user input errors, validating function return values, and ensuring the program is robust against issues.'),
(10, 'Level 10', 'Apply the knowledge gained from previous levels to create full programs such as games, calculators, or user-driven applications, utilizing functions, arrays, loops, and conditional statements effectively.');

INSERT INTO quest (level_id, quest_q, quest_a, spoil, hint) VALUES
(1, 
 "Write a program that declares an integer variable to store the number of hearts in a game. Your program should print 'You have 5 hearts at the start.' to the console.", 
 "You have 5 hearts at the start.",
 "You have just left your home and ventured into the dark, mysterious forest. Your journey is dangerous, and you only have 5 hearts to begin with. In this quest, you need to write a program that declares an integer variable to store the number of hearts you have, reminding you of your limited health as you continue your adventure.", 
 "Use 'int' to declare an integer variable, and 'printf()' to print it."
),
(1, 
 "Write a program that prints 'Hello, world!' to the console. This is your first greeting as you begin your adventure in the forest.", 
 "Greeting: Hello, world!",
 "After leaving your home, you begin your adventure in the wild, where you meet all kinds of animals. you decide to greet them with a friendly 'Hello, world!' in the forest. In this first level, you need to write a simple program that prints a greeting to the console.", 
 "Remember to use printf() to print text to the console."
),
(2, 
 "Write a program that calculates the result of an expression. The final answer should be 39.", 
 "Result: 39",
 "Your adventure through the forest continues, and you've come across another mysterious box. This box is locked by a puzzle. It seems that the solution to the puzzle is a simple expression involving numbers and operations. In this quest, you'll learn how to use expressions in C. Expressions allow you to calculate values, and they're the key to unlocking this box.", 
 "You can use *, +, and - to perform multiplication, addition, and subtraction, respectively. Keep in mind the order of operations."
),
(2, 
 "Write a program that calculates the result of an expression involving two numbers. Your goal is to compute the result of the expression (15 + 5) * 4 - 20 / 5 and The final result should be Result: 203.", 
 "Result: 203",
 "As you travel deeper into the forest, you encounter a new box — this time, it’s locked with a more complex puzzle. you will need to combine multiple operations. In this quest, you'll learn how to combine different operators in one expression. Get ready for a challenge!", 
 "You can use *, +, and - to perform multiplication, addition, and subtraction, respectively. Keep in mind the order of operations."
),
(3, 
 "Write a program that checks if you are good at jumping. The program should print only 'I am the best jumper!'", 
 "I am the best jumper!",
 "In the adventure through the forest, you will face many obstacles. Good jumping skills are essential to overcome these challenges. We believe that you are the best at jumping over obstacles. This quest will require you to use an if statement to check you are good at jumping.", 
 "Use an if statement to check if you are good at jumping. If the condition is true, print: 'I am the best jumper!' Otherwise, print: 'I am not good at jumping.'"
),
(3, 
 "Write a program that checks if your speed is enough to jump over a dangerous creature. If the danger level is 5 or more and your speed is less than 5, print 'You couldn't jump over the creature.' Ensure that your result prints only this statement.", 
 "You couldn't jump over the creature.",
 "You are on an adventurous journey through the forest and encounter various creatures. Each creature has a level of danger, and your jumping ability is affected by your speed. If the creature is dangerous and you are fast enough, you can jump over it. Otherwise, Write a program that will help you decide what happens when you encounter these creatures.", 
 "Use if and else if to check two conditions at once. Check the dangerLevel and speed variables."
),
(4, 
 "Use a loop to repeat the action of printing the message multiple times. Set the loop to run 5 times.", 
 "C programming is fun!\nC programming is fun!\nC programming is fun!\nC programming is fun!\nC programming is fun!", 
 "As you continue your journey through the forest, you notice a small, green slime bouncing along the path. But wait… wasn’t that the same slime you saw a moment ago? You keep walking, but no matter which way you turn, the same cute slime seems to appear over and over again. You feel like you’ve been walking in circles. This time, it seems the forest is testing your skills with loops. To continue your journey, you must write a program that prints 'C programming is fun!' five times using a loop.", 
 "Use a for loop with this format: for (initial; condition; update) { // statements }"
),
(4, 
 "Write a program that prints the positions of fire orbs in a grid. Use nested loops to print numbers in the following format: 1 2 3 on the first row, 4 5 6 on the second row, and so on.", 
 "1 2 3\n4 5 6", 
 "The deeper you venture into the forest, the more dangerous it becomes. As you continue, you enter a maze-like area where the ground is covered in patches of glowing fire orbs. These fiery spheres float and explode with dazzling intensity. The orbs appear in a grid pattern, and you must analyze their positions carefully to avoid triggering any explosions. You must navigate the maze by counting the orbs and plotting your movements to stay safe. Focus on the grid and stay sharp.", 
 "Use a nested for loop: the outer loop should represent rows, and the inner loop should print numbers in each row."
),
(5, 
 "Write a program using a while loop to print step numbers from 1 to 5. Print each step with the format 'Step: x,' where x is the step number.", 
 "Step: 1\nStep: 2\nStep: 3\nStep: 4\nStep: 5", 
 "You enter the deep, dark forest at night, the eerie silence broken only by the sounds of creatures moving around. As you move forward, you step on something squishy — it's a slime! You quickly back off, but the path is dangerous. The ground is covered with sharp thorns, and you need to count your steps carefully. You can't afford to step on a thorn and trigger a trap. Count the steps one by one, and be cautious in your movement.", 
 "Use a while loop to print each step number. The loop should stop once you’ve reached step 5."
),
(5, 
 "Write a program using a do-while loop to count and print the number of noises. The format should be 'Noise: x,' where x is the noise count.", 
 "Noise: 1\nNoise: 2\nNoise: 3\nNoise: 4", 
 "The forest gets darker and thicker as you venture deeper. The air feels dense with mystery, and you hear the distant sounds of slimes. The ground is covered in thorns, but you're too focused to stop. Suddenly, a series of loud, creaking noises start. Each noise comes from the deep, hidden traps in the forest floor. To make sure you don’t fall into one of these traps, you must count how many times you hear the noise, but you must start counting at least once, no matter what.", 
 "Use a do-while loop to count the noises. The loop will execute at least once before checking if it should stop."
),
(6, 
 "Write a program that defines and calls a function named printStep. The function should take an integer as input, representing the step number, and print it in the format 'Safe Step: x,' where x is the step number. Call the function 3 times with step numbers 1, 2, and 3.", 
 "Safe Step: 1\nSafe Step: 2\nSafe Step: 3", 
 "As you sprint through the dark forest, the ground beneath your feet begins to shift unpredictably. You realize this is no ordinary path—it's enchanted, constantly rearranging itself like a giant puzzle. The glowing slimes in the distance urge you to keep moving forward. To navigate this treacherous terrain, you must repeatedly call on a guiding spell that tells you where the ground is safe to step. The spell is simple, but you must invoke it every time to keep moving.",
 "Define a function that accepts an integer parameter. Use printf to format the output, and call the function with different inputs."
),
(6, 
 "Write a program that defines and calls a function named calculateEnergy. The function should take an integer representing the number of steps and return the total energy required. The energy is calculated as steps * 10. Call the function with 3 steps and print the result in the format 'Total Energy: x,' where x is the energy.", 
 "Total Energy: 30", 
 "You’re getting closer to the edge of the forest, but the path is still full of surprises. Slimes and glowing fireflies hover around, distracting you as the ground beneath continues to move. To reach the exit, you must calculate the total energy required to keep running on this unstable path. Using a simple calculation spell, determine the total energy based on how many steps you've taken. The calculation must be precise—no room for error now!",
 "Define a function that accepts an integer parameter, performs a calculation, and returns the result. Use the return statement to pass the value back to main."
),
(7, 
 "Write a program that creates an array containing the numbers of five houses (101, 202, 303, 404, 505). Print the number of the third house. Format the output as 'The third house number is: x,' where x is the number.", 
 "The third house number is: 303", 
 "You cautiously enter the deserted village, the faint creak of wooden doors swaying in the wind filling the air. Along the path, you notice that each abandoned house has a number etched above its doorway. As you pass by, you realize these numbers are clues left behind by previous travelers. However, you must decipher the number of the most important house to continue your journey. The only way to identify it is by finding the number at a specific position among the houses. Stay alert—monsters lurk in the shadows, and there's no room for error.",
 "Arrays are zero-indexed, so the third house is located at index 2. Use printf to display the number at the specified index. Remember, array indexing starts at 0."
),
(7, 
 "Write a program that creates an array containing six clue numbers (5, 15, 25, 35, 45, 55). Use a loop to print all the clue numbers in the array, formatted as 'Clue: x' where x is the number. Remember, array indexing starts at 0.", 
 "Clue: 5\nClue: 15\nClue: 25\nClue: 35\nClue: 45\nClue: 55", 
 "The deeper you explore the eerie village, the more you feel eyes watching from the darkness. To escape, you must locate hidden clues scattered across several abandoned houses. You discover a list of numbers hidden in one of the houses, representing coordinates to the next safe path. However, the list isn't in plain sight—you must carefully inspect and display every value to uncover the full set of clues. Stay vigilant, as every step closer to the truth also brings you closer to danger.",
 "Use a for loop to iterate through the array. The loop should start at index 0 and go up to the size of the array. (Use this pattern array name[i] to print array)"
),
(8, 
 "Write a C program to simulate your journey through 5 segments of the path. Use a for loop to iterate through segments, starting from segment 1 up to segment 5. At each segment: If the segment number is even, print 'Segment x: Firework exploded! Stop!'. If the segment number is odd, print 'Segment x: Safe! Keep moving.'", 
 "Segment 1: Safe! Keep moving.\nSegment 2: Firework exploded! Stop!\nSegment 3: Safe! Keep moving.\nSegment 4: Firework exploded! Stop!\nSegment 5: Safe! Keep moving.", 
 "Halfway through the deserted village, the path ahead suddenly bursts into chaos as fireworks explode from hidden traps. The colorful blasts light up the dark sky but also make it dangerous to proceed. You must carefully calculate whether it's safe to run through each segment of the path. If a firework explodes at a given point, you must stop and wait before continuing. Use your wits and focus to determine when it's safe to advance without getting caught in the trap!",
 "Use a for loop to go through segment numbers 1 to 5. Inside the loop, use an if-else statement to check if the segment number is odd or even using the modulus operator (%)."
),
(8, 
 "Write a program that uses a function to disarm fireworks. The program should: Create a function disarmFirework that takes two parameters: the firework number and the remaining number of fireworks. The function should print: 'Firework x disarmed. y fireworks remain.' If no fireworks remain, the function should print: 'Firework x disarmed. All fireworks disarmed! The monster is vulnerable!'", 
 "Firework 1 disarmed. 4 fireworks remain.\nFirework 2 disarmed. 3 fireworks remain.\nFirework 3 disarmed. 2 fireworks remain.\nFirework 4 disarmed. 1 fireworks remain.\nFirework 5 disarmed. All fireworks disarmed! The monster is vulnerable!", 
 "As you run toward the edge of the village, a monster blocks your escape. The path ahead is lined with ancient firework traps that must be disarmed before the monster becomes vulnerable. Each firework is connected to a fuse, and you must carefully disarm them one by one. To succeed, you must use a function to manage the disarming process and track your progress until all fireworks are neutralized.",
 "Use a for or while loop in main to iterate through each firework. The loop should calculate the remaining fireworks and pass the correct arguments to the disarmFirework function."
),
(9, 
 "Write a program that simulates the candles along the path. The program should print 'Trap! The path is blocked.' for cursed candles and 'Safe! You may proceed.' Additionally, if a cursed candle is stepped on, it will block the way, and you need to track the number of steps taken before finding a safe candle. The program should print 'Total steps taken: x'.", 
 "Safe! You may proceed.\nSafe! You may proceed.\nTrap! The path is blocked.\nTotal steps taken: 3", 
 "You enter the dark castle, and you notice the faint glow of candles placed along the way. As you step forward, you must carefully avoid the cursed candles, or risk triggering a trap. Every time you step on a cursed candle, the path ahead will be blocked, and you’ll have to backtrack. Some candles are safe, but you don't know which. You must step carefully!", 
 "Use an array to represent the candles along the path. The first and fourth candles are cursed (represented by 1), and others are safe (represented by 0). If you step on a trap, print a message and end the program with break. Otherwise, print how many steps you’ve taken."
),
(9, 
 "Write a program that prints whether each ball is a trap or safe using a for loop. As you step on each ball, print 'Step! Safe ball.' if it’s safe and 'Step! Red ball! Back to start.' if it’s a red ball. The program should stop immediately if you step on a red ball and reset your step count to 0 using break. The program should print 'Total steps taken: x'. If you find a safe ball, continue to the next one.", 
 "Step! Safe ball.\nStep! Red ball! Back to start.\nTotal steps taken: 0", 
 "You find yourself in a maze filled with red balls scattered across the floor. You must avoid stepping on red balls, or you’ll be sent back to the beginning of the maze. To escape, you need to find the safe path through the maze by navigating over safe balls only. The maze is represented by an array of 1s (red balls) and 0s (safe balls).", 
 "Use a for loop to iterate through the array of balls and check each one. Keep track of your steps. If you step on a red ball, reset the count with break, otherwise continue to the next ball using continue."
),
(10, 
 "The floor alternates between rising (1) and falling (0). Print the floor's state at each step. If the floor is rising, skip that step using continue and don't count it. Proceed until you've made 10 steps. Create a function to check and print the floor's state for each step. Each step should be printed on a new line with the text format: Step x: The floor is rising/falling.", 
 "Step 1: The floor is rising.\nStep 2: The floor is falling.\nStep 3: The floor is rising.\nStep 4: The floor is falling.\nStep 5: The floor is rising.\nStep 6: The floor is falling.\nStep 7: The floor is rising.\nStep 8: The floor is falling.\nStep 9: The floor is rising.\nStep 10: The floor is falling.", 
 "As you step deeper into the castle, the floor beneath you starts to shift unexpectedly. Some areas rise, while others sink, creating a moving path that forces you to time your steps carefully. The only way to navigate through the maze is to keep an eye on the shifting areas and move swiftly when the path stabilizes. However, the shifting is unpredictable, and you need to decide quickly whether to take the current path or wait for a safer moment. The monsters are closing in, and the exit is just ahead—but will you make it through the moving floor in time?", 
 "Use a while loop to simulate the floor’s movement. Alternate between rising (1) and falling (0) using a counter. If the floor is rising, use continue to skip the step and move to the next one. You will need to create a function to check the floor's state and print it."
),
(10, 
 "You need to avoid red balls by stepping over them. The balls are positioned at specific steps. If you encounter a red ball, skip that step and continue. Print a message each time you successfully step past a ball or when you encounter one. Proceed until you've completed 15 steps. Each step should be printed with the format: Step x: Stepped over the ball safely./Red ball encountered! Skipping this step.", 
 "Step 1: Stepped over the ball safely.\nStep 2: Stepped over the ball safely.\nStep 3: Red ball encountered! Skipping this step.\nStep 4: Stepped over the ball safely.\nStep 5: Red ball encountered! Skipping this step.\nStep 6: Stepped over the ball safely.\nStep 7: Red ball encountered! Skipping this step.\nStep 8: Stepped over the ball safely.\nStep 9: Stepped over the ball safely.\nStep 10: Red ball encountered! Skipping this step.\nStep 11: Stepped over the ball safely.\nStep 12: Red ball encountered! Skipping this step.\nStep 13: Stepped over the ball safely.\nStep 14: Stepped over the ball safely.\nStep 15: Stepped over the ball safely.", 
 "You are almost at the castle exit, but the path is blocked by several red balls that are rolling around the ground. These balls are part of an ancient trap, and the only way forward is to avoid stepping on them. As you walk cautiously, you notice that the red balls are scattered in specific spots along the path. You need to step carefully, avoiding the balls to keep moving forward. Each time you step over a red ball, you must skip that step and continue. The exit is just ahead, but the challenge is far from over. Can you dodge the traps and make it to safety?", 
 "Use a for loop to simulate walking through the path. Define an array that holds the positions of the red balls. If the current step is in the array, skip it using continue. For each step, print whether you've stepped over a ball or avoided one."
);
