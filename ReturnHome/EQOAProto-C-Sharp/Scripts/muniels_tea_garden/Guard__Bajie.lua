function event_say()
diagOptions = {}
    npcDialogue = "Welcome to the garden. Mind your manners while you're with us or you'll be getting a face full of my fist."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end