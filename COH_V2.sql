use cohcodeV2;

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
    PRIMARY KEY (quest_id, level_id),
    FOREIGN KEY (level_id) REFERENCES levels(level_id)
);

CREATE TABLE correct (
    quest_id INT NOT NULL,
    user_id INT NOT NULL,
    is_correct BOOLEAN NOT NULL DEFAULT false,
    PRIMARY KEY (quest_id, user_id),
    FOREIGN KEY (quest_id) REFERENCES quest(quest_id),
    FOREIGN KEY (user_id) REFERENCES user(user_id)
);

CREATE TABLE includes (
    level_id INT NOT NULL,
    enemy_id INT NOT NULL,
    PRIMARY KEY (level_id, enemy_id),
    FOREIGN KEY (level_id) REFERENCES levels(level_id),
    FOREIGN KEY (enemy_id) REFERENCES enemy(enemy_id)
);


INSERT INTO levels (level_name, description) VALUES 
('Level 1', 'Beginner level with basic challenges'),
('Level 2', 'Introduction to intermediate concepts'),
('Level 3', 'Increased difficulty with puzzles'),
('Level 4', 'Challenges that involve enemy encounters'),
('Level 5', 'Halfway point with strategic tasks'),
('Level 6', 'Higher complexity with special rewards'),
('Level 7', 'Preparation for advanced levels'),
('Level 8', 'Advanced challenges with tough enemies'),
('Level 9', 'Pre-final level with maximum difficulty'),
('Level 10', 'Final showdown with the strongest enemies');


INSERT INTO quest (level_id, quest_q, quest_a) VALUES 
(1, 'Defeat the goblin in the forest', 'Use the sword to attack from behind'),
(1, 'Find the hidden key in the cave', 'The key is under a rock near the entrance'),
(2, 'Collect 5 herbs from the meadow', 'Look near the river banks'),
(2, 'Defeat the bandits at the bridge', 'Use the bow for long-range attacks'),
(3, 'Solve the puzzle to open the gate', 'Align the tiles to match the pattern'),
(3, 'Rescue the captured villager', 'Use the rope to climb the tower'),
(4, 'Find the missing map piece', 'Search the old ruins for clues'),
(4, 'Defeat the skeletons guarding the gate', 'Use a shield to block and counter-attack'),
(5, 'Deliver the message to the outpost', 'Take the shortcut through the forest'),
(5, 'Protect the caravan from ambush', 'Set up traps along the path'),
(6, 'Find the ancient artifact', 'It is hidden behind the waterfall'),
(6, 'Defeat the sorcerer in the tower', 'Use a magical barrier to deflect spells'),
(7, 'Escort the merchant safely', 'Avoid the traps in the canyon'),
(7, 'Retrieve the stolen goods', 'Chase the thief through the alleyways'),
(8, 'Defeat the guardian at the gate', 'Use fire magic to break the shield'),
(8, 'Find the secret passage in the castle', 'The entrance is behind the throne'),
(9, 'Recover the enchanted gem', 'Look in the dark forest during night'),
(9, 'Defeat the dragon’s minions', 'Use ice arrows to slow them down'),
(10, 'Defeat the final boss', 'Use all skills you have learned'),
(10, 'Save the kingdom from destruction', 'Combine the artifacts to unlock power');


INSERT INTO enemy (enemy_name, type, enemy_description) VALUES 
('Goblin', 'Beast', 'A small but vicious creature found in the forests.'),
('Bandit', 'Human', 'A rogue fighter lurking around dangerous areas.'),
('Skeleton Warrior', 'Undead', 'A reanimated skeleton wielding a rusty sword.');



