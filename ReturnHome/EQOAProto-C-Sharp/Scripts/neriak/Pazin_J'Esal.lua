function  event_say(choice)
diagOptions = {}
    npcDialogue = "Have you tried eating the meat of the cave minnows? They are amazing and can be a quick meal if you are starving."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end