function  event_say(choice)
diagOptions = {}
    npcDialogue = "The nightly raids from the gnolls from the west are getting old fast. I don't see us making it here much longer."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end