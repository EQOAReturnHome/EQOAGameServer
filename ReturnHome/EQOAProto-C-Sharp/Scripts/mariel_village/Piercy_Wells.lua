function  event_say(choice)
diagOptions = {}
    npcDialogue = "A pack of tainted wolves have invaded the forest east of here. They are vicious, disease spreading beasts. Please be cautious playerName."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end