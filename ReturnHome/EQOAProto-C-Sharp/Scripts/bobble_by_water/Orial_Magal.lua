function  event_say(choice)
diagOptions = {}
    npcDialogue = "I was wounded in the battle at the River Saren, and had to run home to heal. I hope the deputies in the fight made it back ok..."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end