function  event_say(choice)
diagOptions = {}
    npcDialogue = "To become a tailor, you must become one with the needle and thread. Interested in learning the ways of tailoring?"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end