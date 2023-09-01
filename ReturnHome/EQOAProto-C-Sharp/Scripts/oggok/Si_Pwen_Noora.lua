function  event_say(choice)
diagOptions = {}
    npcDialogue = "The shadow realm must not be taking lightly, playerName. To embrace the power it grants, you must intake the darkness into your heart. It must fuse with your blood. If this does not kill you, then you may have a chance at surviving the magic of necromancy."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end