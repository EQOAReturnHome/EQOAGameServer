function  event_say(choice)
diagOptions = {}
    npcDialogue = "I may impart the ways of jewelcrafting, if you have the passion for it. What do you say, playerName?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end