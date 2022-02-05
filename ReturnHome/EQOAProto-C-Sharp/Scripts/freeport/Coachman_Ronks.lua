local playerCoaches = {
    highpass_coach = "Get me a horse to Highpass.%",
    bobble_coach = "Get me a horse to Bobble By Water.%",
    tea_garden_coach = "Get me a horse to Muniel's Tea Garden.%",
    neriak_coach = "Get me a horse to the dark city of Neriak.%"
}
local coaches = {
    Freeport = {world = 0, x = 25273.03125, y = 54.125, z = 15723.29102, facing = 3.138683081},
    Highpass = {world = 0, x = 16776.640625, y = 187.8125, z = 15351.3916015625, facing = 0.8122978806495667}
}
local ch = tostring(choice)

function event_say()
    SetPlayerFlags(mySession, "highpass_coach", true)
    SetPlayerFlags(mySession, "bobble_coach", true)
    SetPlayerFlags(mySession, "tea_garden_coach", true)
    SetPlayerFlags(mySession, "neriak_coach", true)

    if (GetPlayerFlags(mySession, "freeport_coach")) then
        if (ch:find("Highpass")) then
            npcDialogue = "Going to Highpass it is"
            TeleportPlayer(
                mySession,
                coaches.Highpass.world,
                coaches.Highpass.x,
                coaches.Highpass.y,
                coaches.Highpass.z,
                coaches.Highpass.facing
            )
        elseif (ch:find("Tea")) then
            npcDialogue =
                "Coachman Ronks: I'm gonna give you this here horse, but it has no name. Now you be careful traveling through that desert."
            TeleportPlayer(mySession, 0, 23359.685546875, 54.12599563598633, 19635.919921875, 1.325449824333191)
        elseif (ch:find("Bobble")) then
            npcDialogue = "Off you go to Bobble By Water"
            TeleportPlayer(mySession, 0, 24654.671875, 54.12599563598633, 11853.1748046875, -0.8971897959709167)
        elseif (ch:find("Neriak")) then
            npcDialogue = "Off you go to Neriak"
            TeleportPlayer(mySession, 0, 24924.66797, 29.43331718, 9420.388672, 0.08655221)
        else
            npcDialogue = "Where would you like to go?:::"
            for coach, dialogue in pairs(playerCoaches) do
                if (GetPlayerFlags(mySession, coach)) then
                    npcDialogue = npcDialogue .. dialogue
                end
            end
        end
    elseif (ch:find("Yes")) then
        npcDialogue = "Excellent, you can now use this coach any time."
        SetPlayerFlags(mySession, "freeport_coach", true)
    elseif (ch:find("No")) then
        npcDialogue = "If you aren't interested then why are you wasting my time."
    else
        npcDialogue = "Would you like to sign the coachman's ledger?:::Yes%No:::"
    end
end
