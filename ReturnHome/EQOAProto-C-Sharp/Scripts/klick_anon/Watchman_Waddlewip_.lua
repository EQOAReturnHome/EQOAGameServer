function  event_say(choice)
diagOptions = {}
    npcDialogue = "These doorways can be shut at a moments notice to defend Klick'Anon against our would-be enemies."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end