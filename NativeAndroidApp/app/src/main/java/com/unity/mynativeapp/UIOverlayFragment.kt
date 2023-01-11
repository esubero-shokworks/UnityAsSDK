package com.unity.mynativeapp

import android.Manifest
import android.os.Bundle
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import androidx.activity.result.contract.ActivityResultContracts
import androidx.fragment.app.Fragment

import com.unity.mynativeapp.databinding.FragmentUIOverlayBinding
import com.unity3d.player.UnityPlayer

class UIOverlayFragment : Fragment() {

    private lateinit var binding: FragmentUIOverlayBinding
    private var mUnity: UnityCommunication? = null
    private var isCameraRecording: Boolean = false

    private var resultPermissions =
        registerForActivityResult(ActivityResultContracts.RequestMultiplePermissions()) { permissions ->
            var isGrantAll = false
            permissions.entries.forEach {
                val permissionName = it.key
                isGrantAll = it.value
            }
            if (isGrantAll) {
//                binding.cvVideo.open()
            } else {
                requireActivity().finish()
            }
        }

    override fun onCreateView(
        inflater: LayoutInflater, container: ViewGroup?,
        savedInstanceState: Bundle?
    ): View {
        binding = FragmentUIOverlayBinding.inflate(inflater, container, false)
        return binding.root
    }

    override fun onViewCreated(view: View, savedInstanceState: Bundle?) {
        super.onViewCreated(view, savedInstanceState)
        camera()
//        requestPermissions()
    }

    private fun camera() =
        binding.apply {
//            cvVideo.setLifecycleOwner(viewLifecycleOwner)
            tvFlip.setOnClickListener {
                //UnityPlayer.UnitySendMessage("Service Manager", "DoInstantiateARObject", "SM_Bld_Bridge_Chair_01.obj")
                //println("Sending the DoInstantiateARObject to Unity")

                UnityPlayer.UnitySendMessage("Service Manager", "DoChangeARCamera", "")
                println("Sending the DoChangeCamera to Unity")
                //                cvVideo.toggleFacing()
            }
            lavRecordBtn.setOnClickListener(){
                //UnityPlayer.UnitySendMessage("Service Manager", "DoDestroyARObject", "")
                //println("Sending the DoDestroyARObject to Unity")
                
                isCameraRecording = !isCameraRecording
                if (isCameraRecording){
                    UnityPlayer.UnitySendMessage("Service Manager", "DoStartCameraRecording", "")
                    println("Sending the DoStartCameraRecording to Unity")
                }
                else{
                    UnityPlayer.UnitySendMessage("Service Manager", "DoStopCameraRecording", "")
                    println("Sending the DoStopCameraRecording to Unity")
                }
            }
            ivClose.setOnClickListener {
                mUnity?.finished()
            }
        }

    private fun requestPermissions() =
        resultPermissions.launch(
            arrayOf(
                Manifest.permission.RECORD_AUDIO,
                Manifest.permission.CAMERA,
                Manifest.permission.READ_EXTERNAL_STORAGE
            )
        )

    fun setUnityPlayer(unity: UnityCommunication) {
        mUnity = unity
    }

}

interface UnityCommunication {
    fun showMain()
    fun sendMsg()
    fun unload()
    fun finished()
}