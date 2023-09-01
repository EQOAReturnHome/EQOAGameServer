function event_say()
diagOptions = {}
    npcDialogue = "Our eyes shall remain firmly upon you until your allegiance is proven beyond all doubt."
SendDialogue(mySession, npcDialogue, diagOptions, thisEntity.CharName)
end