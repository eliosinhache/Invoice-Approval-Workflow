<template>
    <v-container class="pa-4" fluid>
        <v-row class="mb-12 mt-4" dense justify="center">
            <v-col cols="12" md="6">
            <v-card
                class="py-4"
                rounded="lg"
            >
                <template #title>
                <h2 class="text-h5 font-medium text-center">
                    Invoice Approval Form
                </h2>
                </template>
                <v-card-text>
                    <v-form ref="form" v-model="valid"  @submit.prevent>
                    <v-row>
                    <v-col cols="12" md="12">
                        <v-col cols="12" md="12">
                        <v-number-input :rules="amountRules" control-variant="hidden" v-model="formData.amount" :precision="2" variant="outlined" hide-details="auto"  :min="1" label="Amount"></v-number-input>
                        </v-col>

                        <v-col cols="12" md="12">
                            <v-checkbox
                                v-model="formData.isPreferredVendor"
                                color="primary"
                                label="Is Preferred Vendor"
                                value="primary"
                                hide-details
                            ></v-checkbox>
                        </v-col>
                    </v-col>
                    </v-row>
                    </v-form>
                </v-card-text>
                
                <v-card-actions class="ma-4" >
                    <v-btn text="Reset" :loading="loading" variant="tonal" @click="reset"></v-btn>

                    <v-spacer></v-spacer>

                    <v-btn text="Send"  :loading="loading"  type="submit" color="green" variant="elevated" @click="send"></v-btn>
                </v-card-actions>
            </v-card>
            </v-col>
        </v-row>    
    </v-container>
</template>
<script setup lang="ts">
import { ref } from 'vue';

const formData = ref({
  amount: null,
  isPreferredVendor: false
});
const amountRules = ref([
  (v: number) => !!v || 'Amount is required',
  (v: number) => (v && v > 0) || 'Amount must be greater than zero',
]); 
const valid = ref(false);
const loading = ref(false);
const form = ref();

async function send() {
    const result = await form.value.validate()
    valid.value = result.valid;
    if (!valid.value) return;
    
    loading.value = true;
    console.log('send amount:');
    loading.value = false;
}

function reset() {
    form.value.reset();
}
</script>