function  event_say(choice)
diagOptions = {}
    npcDialogue = "Armorcrafting is a hot and dirty job but ye can make a fortune. Too bad the Tanglefoots dip their greedy hands into my pockets."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end