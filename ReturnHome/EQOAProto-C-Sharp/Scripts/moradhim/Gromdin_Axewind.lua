function  event_say(choice)
diagOptions = {}
    npcDialogue = "It's the Baga trolls I'm concerned with. They've been taking out our brothers with some new weapon. We must find out just what it is they're using against us. Perhaps the rangers to the south could help us�"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end