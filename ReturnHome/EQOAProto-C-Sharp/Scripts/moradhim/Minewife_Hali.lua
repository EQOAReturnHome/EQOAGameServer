function  event_say(choice)
diagOptions = {}
    npcDialogue = "When ma husband comes home he likes his weed pipe and ale ready right away. He's free to rest only fer a moment or two, but then he's also in charge of dinner. I need a life too ya know."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end