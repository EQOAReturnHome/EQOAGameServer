function  event_say(choice)
diagOptions = {}
    npcDialogue = "The ignorance you've just shown by barging in here is unprecedented. Leave before I get angry, fool."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end