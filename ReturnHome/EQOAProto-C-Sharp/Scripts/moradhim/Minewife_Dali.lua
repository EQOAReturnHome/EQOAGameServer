function  event_say(choice)
diagOptions = {}
    npcDialogue = "Where are all the other lady dwarfs? Did ye not know? Male dwarves outnumber female dwarves two to one. It's just the nature of our people."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end