function  event_say(choice)
diagOptions = {}
    npcDialogue = "These damned boots really get to my feet now. Standin' 'ere all day. Where's the action??"
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end